using System.Collections.ObjectModel;
using TemplateBuilder.Model;

namespace TemplateBuilder.ViewModel.Interfaces
{
    public interface IToolboxViewModel
    {
        ObservableCollection<CustomControl> Controls { get; set; }
    }
}