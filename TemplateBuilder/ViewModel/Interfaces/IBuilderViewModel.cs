namespace TemplateBuilder.ViewModel.Interfaces
{
    public interface IBuilderViewModel
    {
        IProjectViewModel Project { get; set; }
        IToolboxViewModel Toolbox { get; set; }
    }
}
