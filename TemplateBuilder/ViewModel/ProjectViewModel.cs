using GalaSoft.MvvmLight;
using TemplateBuilder.ViewModel.Interfaces;

namespace TemplateBuilder.ViewModel
{
    public class ProjectViewModel : ViewModelBase, IProjectViewModel
    {
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
    }
}
