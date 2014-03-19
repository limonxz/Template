using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using TemplateBuilder.Model;
using TemplateBuilder.Model.Messages;
using TemplateBuilder.ViewModel.Interfaces;

namespace TemplateBuilder.ViewModel
{
    public class ProjectViewModel : ViewModelBase, IProjectViewModel
    {
        #region Properties

        /// <summary>
        /// To Store the controls
        /// </summary>
        public ObservableCollection<Control> Controls { get; set; }

        private string _name;
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

        double _XPos, _YPos;

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
        /// Command Action: PanelDrop
        /// Command action, use for get the item drop
        /// </summary>
        void Execute_PanelDrop(DragEventArgs e)
        {
            var container = (Panel)e.Source;
            var data = (CustomControl)e.Data.GetData(typeof(CustomControl));

            if (data != null && data.TheControl != null)
            {
                data.TheControl.HorizontalAlignment = HorizontalAlignment.Left;

                var ctrl = data.TheControl.XamlClone();
                ctrl.PreviewMouseLeftButtonDown += MouseLeftButtonDown;
                ctrl.PreviewMouseMove += MouseMove;
                ctrl.Cursor = Cursors.Hand;

                Controls.Add(ctrl);
                container.Children.Add(ctrl);
            }
        }

        /// <summary>
        /// Event to get the first position and control selected
        /// </summary>
        void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ctrl = sender as Control;
            var ctrlPos = e.GetPosition(ctrl);

            /// TODO: Get these values from View
            var dockWidth = 120;
            var dockheader = 20;

            _XPos = ctrlPos.X + dockWidth;
            _YPos = ctrlPos.Y + dockheader;

            ItemSelected = (Control)sender;
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
                var ctrlParent = ctrl.Parent as Control;
                var ctrlParentPos = e.GetPosition(ctrlParent);

                ctrl.SetValue(Canvas.LeftProperty, ctrlParentPos.X - _XPos);
                ctrl.SetValue(Canvas.TopProperty, ctrlParentPos.Y - _YPos);
            }
        }

        #endregion

        #region ctor

        public ProjectViewModel()
        {
            Controls = new ObservableCollection<Control>();
        }

        #endregion
    }
}
