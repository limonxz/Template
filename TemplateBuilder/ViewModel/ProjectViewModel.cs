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

        /// <summary>
        /// to manage the position
        /// </summary>
        double _XPos, _YPos, _ArrowXPos, _ArrowYPos;

        /// <summary>
        /// The current view
        /// </summary>
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

                    var controlMessage = new ControlMessage() { TheControl = value };
                    Messenger.Default.Send(controlMessage);
                }
            }

        /// <summary>
        /// Panlel Main Container
        /// </summary>
        Panel _Container;
        
        public IEditableCanvasViewModel EditableCanvas { get; set; }
        public EditableCanvasView EditableCanvasView { get; set; }

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
        /// Action to create a new template
        /// </summary>
        void NewProject(NewProjectMessage obj)
        {
            if (_Container != null && _Container.Children.Count > 0)
            {
                if (MessageBox.Show("Do you want save the current Template?", "Template",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    var saveProjectMessage = new SaveProjectMessage();
                    SaveProject(saveProjectMessage);
                }

                ItemSelected = null;
                _Container.Children.Clear();
            }
        }

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
                if (File.Exists(msg.FilePath))
                {
                    File.Delete(msg.FilePath);
                }

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
                    AddControlsContainer(msg.CurrrentContainer);
                }
            }
        }

        /// <summary>
        /// Update control text after editing
        /// </summary>
        void UpdateControlContent(FinishEditingMessage finishEditingMessage)
        {
            if (ItemSelected is ToggleButton)
                (ItemSelected as ToggleButton).Content = finishEditingMessage.Text;

            if (ItemSelected is Label)
                (ItemSelected as Label).Content = finishEditingMessage.Text;

            _Container.Children.Remove(EditableCanvasView);
            EditableCanvasView = null;
        }

        /// <summary>
        /// set visible editable canvas over control selected
        /// </summary>
        void ShowEditableCanvas(ShowEditableCanvasMessage msg)
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


        /// <summary>
        /// Method to add action to the child controls 
        /// </summary>
        void AddControlsContainer(Panel panel)
        {
            foreach (UIElement item in panel.Children)
                    {
                        var ctrl = item.XamlClone() as Control;
                        SetActionsControl(ref ctrl);
                        _Container.Children.Add(ctrl);
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

            _XPos = ctrlPosition.X;
            _YPos = ctrlPosition.Y;

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
                ctrl.SetValue(Canvas.LeftProperty, ctrlPos.X - _XPos);
                ctrl.SetValue(Canvas.TopProperty, ctrlPos.Y - _YPos);
            }
        }

        /// <summary>
        /// For set the actions when moves the control
        /// </summary>
        void SetActionsControl(ref Control ctrl)
        {
            ctrl.PreviewMouseLeftButtonDown += MouseLeftButtonDown;
            ctrl.PreviewMouseMove += MouseMove;
            ctrl.PreviewKeyDown += KeyDown;
            ctrl.Cursor = Cursors.Hand;
            ctrl.Focusable = true;
            ctrl.Uid = string.Format("Uid{0}", _Container.Children.Count + 1);
            
            if (ctrl is ToggleButton || ctrl is Label)
            {
                ctrl.PreviewMouseDoubleClick += CtrlOnPreviewMouseDoubleClick;
                ctrl.MouseDoubleClick += MouseDoubleClick;
            }
        }

        /// <summary>
        /// To delete current control
        /// </summary>
        void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Do you want eliminate this control?", "Are you sure?",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    var ctrl = sender as UIElement;

                    if (_Container.Children.Contains(ctrl))
                    {
                        _Container.Children.Remove(ctrl);
                    }

                    ItemSelected = null;
                }
            }
        }

        /// <summary>
        /// Assign the selected control when doubleclick
        /// </summary>
        private void CtrlOnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (sender is ToggleButton || sender is Label)
                ItemSelected = (Control)sender;
        }

        /// <summary>
        /// Paint the canvas over the control selected on doubleclick
        /// </summary>
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
            Messenger.Default.Register<NewProjectMessage>(this, NewProject);
            Messenger.Default.Register<OpenProjectMessage>(this, OpenProject);
            Messenger.Default.Register<SaveProjectMessage>(this, SaveProject);
            Messenger.Default.Register<ProjectContainerMessage>(this, GetProjectContainer);
            Messenger.Default.Register<FinishEditingMessage>(this, UpdateControlContent);
            Messenger.Default.Register<ShowEditableCanvasMessage>(this, ShowEditableCanvas);
        }

        #endregion
    }
}
