using GalaSoft.MvvmLight;
using TemplateBuilder.Model;
using TemplateBuilder.ViewModel.Interfaces;

namespace TemplateBuilder.ViewModel
{
    public class PropertiesViewModel : ViewModelBase, IPropertiesViewModel
    {
        public CustomControl SelectedControl { get; set; }
    }
}
