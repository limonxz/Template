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
            var data = (CustomControl)e.Data.GetData(typeof(CustomControl));

            if (data != null && data.TheControl != null)
            {
                data.TheControl.HorizontalAlignment = HorizontalAlignment.Left;
                Controls.Add(data.TheControl.XamlClone());
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
