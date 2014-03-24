using System.Windows.Controls;

namespace TemplateBuilder.ViewModel.Interfaces
{
    public interface IPropertiesViewModel
    {
        /// <summary>
        /// The control selected
        /// </summary>
        Control SelectedControl { get; set; }
    }
}
