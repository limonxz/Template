using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using TemplateBuilder.Model.Messages;
using TemplateBuilder.ViewModel.Interfaces;

namespace TemplateBuilder.ViewModel
{
    public class PropertiesViewModel : ViewModelBase, IPropertiesViewModel
    {
        #region properties

        bool _AllProperties;
        /// <summary>
        /// Propery used to the control of properties
        /// </summary>
        public bool AllProperties
        {
            get { return _AllProperties; }
            set
            {
                _AllProperties = value;
                RaisePropertyChanged(() => this.AllProperties);
            }
        }

        Control _SelectedControl;
        /// <summary>
        /// Propery used to gets the selected control
        /// </summary>
        public Control SelectedControl
        {
            get { return _SelectedControl; }
            set
            {
                _SelectedControl = value;
                RaisePropertyChanged(() => this.SelectedControl);
            }
        } 

        #endregion

        #region ctor
        
        public PropertiesViewModel()
        {
            AllProperties = false;
            Messenger.Default.Register<ControlMessage>(this, SetSelectedControl);
        } 

        #endregion

        /// <summary>
        /// Method to set the selected control
        /// </summary>
        void SetSelectedControl(ControlMessage msg) 
        {
            SelectedControl = msg.TheControl;
        }
    }
}
