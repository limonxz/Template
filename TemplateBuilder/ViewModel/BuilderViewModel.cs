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

        public IProjectViewModel Project { get; set; }
        public IToolboxViewModel Toolbox { get; set; }

        #endregion

        #region Commands
        
        public ICommand SaveProject { get { return new RelayCommand(OnSavingProject); } }
        public ICommand OpenProject { get { return new RelayCommand(OnOpeningProject); } }

        #endregion

        #region Actions

        private void OnSavingProject()
        {
            Messenger.Default.Send(new SaveProjectMessage());
        }

        private void OnOpeningProject()
        {
            Messenger.Default.Send(new OpenProjectMessage());
        }

        #endregion

        #region ctor
        
        public BuilderViewModel()
        {
            this.Project = ServiceLocator.Current.GetInstance<IProjectViewModel>();
            this.Toolbox = ServiceLocator.Current.GetInstance<IToolboxViewModel>();
        }

        #endregion
    }
}
