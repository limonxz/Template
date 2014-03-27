using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using TemplateBuilder.Model;
using TemplateBuilder.Model.Messages;
using TemplateBuilder.ViewModel.Interfaces;
using TemplateBuilder.Views;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace TemplateBuilder.ViewModel
{
    public class ProjectViewModel : ViewModelBase, IProjectViewModel
    {
        #region Properties

        ProjectView _ProjectView;

        /// <summary>
        /// Filter for the file
        /// </summary>
        static string FILEFILTER = "XML Files (*.xml)|*.xml";

        private string _name;
        /// <summary>
        /// The project name
        /// </summary>
        public string Name
        {
            get
            {
                return string.IsNullOrWhiteSpace(_name) ? "Project 1" : _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged(() => this.Name);
            }
        }

        Control _ItemSelected;
        /// <summary>
        /// property used to get the selected item
        /// </summary>
        public Control ItemSelected
        {
            get { return _ItemSelected; }
            set
            {
                _ItemSelected = value;
                RaisePropertyChanged(() => this.ItemSelected);

                if (value != null)
                {
                    var controlMessage = new ControlMessage() { TheControl = value };
                    Messenger.Default.Send(controlMessage);
                }
            }
        }

        /// <summary>
        /// Panlel Main Container
        /// </summary>
        Panel _Container;

        #endregion

        #region Commands

        internal ICommand _PanelDrop;
        /// <summary>
        /// Command action, use for get the item drop
        /// </summary>
        public ICommand PanelDrop
        {
            get
            {
                if (_PanelDrop == null)
                {
                    _PanelDrop = new RelayCommand<DragEventArgs>(Execute_PanelDrop);
                }

                return _PanelDrop;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Action to save the template
        /// </summary>
        void SaveProject(SaveProjectMessage msg)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = FILEFILTER;
            saveFileDialog.ShowDialog();
            msg.FilePath = saveFileDialog.FileName;
            msg.ProjectViewTemplate = _ProjectView;

            msg.ProjectViewTemplate.ProjectTemplate.Children.Remove(EditableCanvasView);

            if (!string.IsNullOrEmpty(msg.FilePath))
            {
                using (var fs = new FileStream(msg.FilePath, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    XamlWriter.Save(msg.ProjectViewTemplate.ProjectTemplate, fs);
                }
            }
        }

        /// <summary>
        /// For open the project
        /// </summary>
        void OpenProject(OpenProjectMessage msg)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = FILEFILTER;
            openFileDialog.ShowDialog();
            msg.FilePath = openFileDialog.FileName;
            msg.ProjectViewTemplate = _ProjectView;

            if (!string.IsNullOrEmpty(msg.FilePath))
            {
                using (var mysr = new StreamReader(msg.FilePath))
                {
                    msg.CurrrentContainer = XamlReader.Load(mysr.BaseStream) as Panel;
                }

                if (msg.CurrrentContainer != null)
                {
                    _Container = msg.ProjectViewTemplate.ProjectTemplate;

                    foreach (UIElement item in msg.CurrrentContainer.Children)
                    {
                        var ctrl = item.XamlClone() as Control;
                        SetActionsControl(ref ctrl);
                        _Container.Children.Add(ctrl);
                    }
                }
            }
        }

        /// <summary>
        /// For get the UIElement
        /// </summary>
        void GetProjectContainer(ProjectContainerMessage msg)
        {
            if (msg != null && msg.ProjectViewContainer != null)
            {
                _ProjectView = msg.ProjectViewContainer;
            }
        }

        /// <summary>
        /// Command Action: PanelDrop
        /// Command action, use for get the item drop
        /// </summary>
        void Execute_PanelDrop(DragEventArgs e)
        {
            if (_Container == null)
            {
                _Container = (Panel)e.Source;
            }

            var data = (CustomControl)e.Data.GetData(typeof(CustomControl));

            if (data != null && data.TheControl != null)
            {
                data.TheControl.HorizontalAlignment = HorizontalAlignment.Left;

                var ctrl = data.TheControl.XamlClone();
                SetActionsControl(ref ctrl);

                var ctrlPos = e.GetPosition(_Container);
                ctrl.SetValue(Canvas.LeftProperty, ctrlPos.X);
                ctrl.SetValue(Canvas.TopProperty, ctrlPos.Y);

                _Container.Children.Add(ctrl);
            }
        }

        private Point _pointPosition;
        private Point _initialMousePosition;
        /// <summary>
        /// Event to get the first position and control selected
        /// </summary>
        void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ctrl = (Control)sender;
            var ctrlPosition = e.GetPosition(ctrl);

            _initialMousePosition = e.GetPosition(_Container);
            _pointPosition = new Point(Canvas.GetLeft(ctrl) - _initialMousePosition.X, 
                Canvas.GetTop(ctrl) - _initialMousePosition.Y);


            ItemSelected = ctrl;
            ItemSelected.Focusable = true;
            ItemSelected.Focus();
        }

        /// <summary>
        /// Event to drag the control selected
        /// </summary>
        void MouseMove(object sender, MouseEventArgs e)
        {
            var ctrl = sender as Control;

            if (e.LeftButton.Equals(MouseButtonState.Pressed)
                && ItemSelected.Equals(ctrl))
            {

                var ctrlPos = e.GetPosition(_Container);
                ctrl.SetValue(Canvas.LeftProperty, ctrlPos.X + _pointPosition.X);
                ctrl.SetValue(Canvas.TopProperty, ctrlPos.Y + _pointPosition.Y);
            }
        }

        /// <summary>
        /// For set the actions when moves the control
        /// </summary>
        void SetActionsControl(ref Control ctrl)
        {
            ctrl.PreviewMouseLeftButtonDown += MouseLeftButtonDown;
            ctrl.PreviewMouseMove += MouseMove;
            ctrl.Cursor = Cursors.Hand;
            ctrl.Focusable = true;

            if (ctrl is ToggleButton || ctrl is Label)
            {
                ctrl.PreviewMouseDoubleClick += CtrlOnPreviewMouseDoubleClick;
                ctrl.MouseDoubleClick += MouseDoubleClick;
            }
        }

        private void CtrlOnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (sender is ToggleButton || sender is Label)
                ItemSelected = (Control)sender;
        }

        private void MouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (sender is ToggleButton || sender is Label)
            {
                var textBlock = (mouseButtonEventArgs.OriginalSource as UIElement);
                var point = textBlock.TransformToAncestor(_Container).Transform(new Point(0, 0));

                var showCanvasMessage = new ShowEditableCanvasMessage(mouseButtonEventArgs.OriginalSource as UIElement, point);
                Messenger.Default.Send(showCanvasMessage);
            }
        }

        #endregion

        #region ctor

        public ProjectViewModel()
        {
            Messenger.Default.Register<OpenProjectMessage>(this, OpenProject);
            Messenger.Default.Register<SaveProjectMessage>(this, SaveProject);
            Messenger.Default.Register<ProjectContainerMessage>(this, GetProjectContainer);
            Messenger.Default.Register<FinishEditingMessage>(this, UpdateControlContent);
            Messenger.Default.Register<ShowEditableCanvasMessage>(this, ShowEditableCanvas);
        }

        private void UpdateControlContent(FinishEditingMessage finishEditingMessage)
        {
            if (ItemSelected is ToggleButton)
                (ItemSelected as ToggleButton).Content = finishEditingMessage.Text;
            
            if(ItemSelected is Label)
                (ItemSelected as Label).Content = finishEditingMessage.Text;

            _Container.Children.Remove(EditableCanvasView);
            EditableCanvasView = null;
        }

        #endregion

        private void ShowEditableCanvas(ShowEditableCanvasMessage msg)
        {
            EditableCanvas = new EditableCanvasViewModel
            {
                Source = msg.Control,
                Position = msg.Position,
                IsVisible = Visibility.Visible
            };

            EditableCanvasView = new EditableCanvasView(EditableCanvas);
            _Container.Children.Add(EditableCanvasView);
        }

        public IEditableCanvasViewModel EditableCanvas { get; set; }
        public EditableCanvasView EditableCanvasView { get; set; }
    }
}
