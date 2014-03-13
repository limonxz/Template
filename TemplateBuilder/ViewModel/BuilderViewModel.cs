using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using TemplateBuilder.Model;
using TemplateBuilder.ViewModel.Interfaces;

namespace TemplateBuilder.ViewModel
{
    public class BuilderViewModel : ViewModelBase, IBuilderViewModel
    {
        public IProjectViewModel Project { get; set; }
        public IToolboxViewModel Toolbox { get; set; }

        public BuilderViewModel()
        {
            this.Project = ServiceLocator.Current.GetInstance<IProjectViewModel>();
            this.Toolbox = ServiceLocator.Current.GetInstance<IToolboxViewModel>();
        }
    }
}
