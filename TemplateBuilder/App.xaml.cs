using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using TemplateBuilder.ViewModel.Interfaces;
using TemplateBuilder.Views;
using UnityConfiguration;

namespace TemplateBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer _container = null;
        internal static IUnityContainer Container
        {
            get { return _container; }
        }

        static App()
        {
            _container = CreateUnityContainer();

            _container.RegisterInstance(_container);

            var locator = new UnityServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var builderViewModel = Container.Resolve<IBuilderViewModel>();
            var mainWindow = Container.Resolve<BuilderView>();
            mainWindow.DataContext = builderViewModel;
            mainWindow.Show();
        }

        static IUnityContainer CreateUnityContainer()
        {
            var unityContainer = new UnityContainer();
            unityContainer.Configure(x => x.Scan(scanner =>
            {
                scanner.AssembliesInBaseDirectory();
                scanner.ForRegistries();
            }));

            return unityContainer;
        }
    }
}
