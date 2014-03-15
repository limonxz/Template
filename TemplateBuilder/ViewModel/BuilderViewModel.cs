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
        public IProjectViewModel Project { get; set; }
        public IToolboxViewModel Toolbox { get; set; }
        public ICommand SaveProject { get { return new RelayCommand(OnSavingProject); } }
        public ICommand OpenProject { get { return new RelayCommand(OnOpeningProject); } }

        public BuilderViewModel()
        {
            this.Project = ServiceLocator.Current.GetInstance<IProjectViewModel>();
            this.Toolbox = ServiceLocator.Current.GetInstance<IToolboxViewModel>();
        }

        private void OnSavingProject()
        {
            Messenger.Default.Send(new SaveProjectMessage());
        }

        private void OnOpeningProject()
        {
            Messenger.Default.Send(new OpenProjectMessage());
        }
    }
}
