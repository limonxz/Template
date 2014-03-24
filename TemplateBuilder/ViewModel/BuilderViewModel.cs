using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using TemplateBuilder.Model.Messages;
using TemplateBuilder.ViewModel.Interfaces;

namespace TemplateBuilder.ViewModel
{
    public class BuilderViewModel : ViewModelBase, IBuilderViewModel
    {
        #region Properties

        /// <summary>
        /// The View Model of the template
        /// </summary>
        public IProjectViewModel Project { get; set; }

        /// <summary>
        /// The View model of the Tool Box
        /// </summary>
        public IToolboxViewModel Toolbox { get; set; }

        /// <summary>
        /// The View model of the Propeerties
        /// </summary>
        public IPropertiesViewModel Properties { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to save the template
        /// </summary>
        public ICommand SaveProject { get { return new RelayCommand(OnSavingProject); } }

        /// <summary>
        /// The command to open the template
        /// </summary>
        public ICommand OpenProject { get { return new RelayCommand(OnOpeningProject); } }

        #endregion

        #region Actions

        /// <summary>
        /// Action to call method of the other VM for save the template
        /// </summary>
        void OnSavingProject()
        {
            var saveProjectMessage = new SaveProjectMessage();
            Messenger.Default.Send(saveProjectMessage);

        }

        /// <summary>
        /// Action to call method of the other VM, for open the template
        /// </summary>
        void OnOpeningProject()
        {
            var openProjectMessage = new OpenProjectMessage();
            Messenger.Default.Send(openProjectMessage);
        }

        #endregion

        #region ctor

        public BuilderViewModel()
        {
            Project = ServiceLocator.Current.GetInstance<IProjectViewModel>();
            Toolbox = ServiceLocator.Current.GetInstance<IToolboxViewModel>();
            Properties = ServiceLocator.Current.GetInstance<IPropertiesViewModel>();
        }

        #endregion
    }
}
