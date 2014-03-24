namespace TemplateBuilder.ViewModel.Interfaces
{
    public interface IBuilderViewModel
    {
        /// <summary>
        /// The Project Tem´plate
        /// </summary>
        IProjectViewModel Project { get; set; }

        /// <summary>
        /// The tool box panel
        /// </summary>
        IToolboxViewModel Toolbox { get; set; }
    }
}
