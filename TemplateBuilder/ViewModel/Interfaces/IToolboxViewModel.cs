using System.Collections.ObjectModel;
using TemplateBuilder.Model;

namespace TemplateBuilder.ViewModel.Interfaces
{
    public interface IToolboxViewModel
    {
        /// <summary>
        /// The Custom controls of the Tool Box
        /// </summary>
        ObservableCollection<CustomControl> Controls { get; set; }
    }
}