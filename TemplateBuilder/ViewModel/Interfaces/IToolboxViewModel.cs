using System.Collections.ObjectModel;
using TemplateBuilder.Model;

namespace TemplateBuilder.ViewModel.Interfaces
{
    public interface IToolboxViewModel
    {
        CustomControl SelectedControl { get; set; }
        ObservableCollection<CustomControl> Controls { get; set; }
    }
}