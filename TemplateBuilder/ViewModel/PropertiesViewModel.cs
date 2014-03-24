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
