using UnityConfiguration;

namespace TemplateBuilder.Infrastructure.Registries
{
    public class ViewModelsRegistry : UnityRegistry
    {
        public ViewModelsRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesInBaseDirectory();
                scan.WithNamingConvention();
            });
        }
    }
}
