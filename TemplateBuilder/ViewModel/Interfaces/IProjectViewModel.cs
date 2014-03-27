namespace TemplateBuilder.ViewModel.Interfaces
{
    public interface IProjectViewModel
    {
        /// <summary>
        /// The Template Name
        /// </summary>
        string Name { get; set; }

        IEditableCanvasViewModel EditableCanvas { get; set; }
    }
}
