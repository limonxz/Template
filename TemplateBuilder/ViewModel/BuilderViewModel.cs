using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using TemplateBuilder.Model.Messages;
using TemplateBuilder.ViewModel.Interfaces;

namespace TemplateBuilder.ViewModel
{
    public class BuilderViewModel : ViewModelBase, IBuilderViewModel
    {
        #region Properties

        public IProjectViewModel Project { get; set; }
        public IToolboxViewModel Toolbox { get; set; }
        public IPropertiesViewModel Properties { get; set; }

        #endregion

        #region Commands
        
        public ICommand SaveProject { get { return new RelayCommand(OnSavingProject); } }
        public ICommand OpenProject { get { return new RelayCommand(OnOpeningProject); } }

        #endregion

        #region Actions

        private void OnSavingProject()
        {
            var saveProjectMessage = new SaveProjectMessage();
            Messenger.Default.Send(saveProjectMessage);

            if (!string.IsNullOrEmpty(saveProjectMessage.FilePath))
            {
                using (var fs = new FileStream(saveProjectMessage.FilePath, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    XamlWriter.Save(saveProjectMessage.ProjectView.ProjectTemplate, fs);
                }

                //saveProjectMessage.ProjectView.ProjectTemplate.Children.Clear();
            }
        }

        private void OnOpeningProject()
        {
            var openProjectMessage = new OpenProjectMessage();
            Messenger.Default.Send(openProjectMessage);

            if (!string.IsNullOrEmpty(openProjectMessage.FilePath))
            {
                using (var mysr = new StreamReader(openProjectMessage.FilePath))
                {
                    var rootObject = XamlReader.Load(mysr.BaseStream) as StackPanel;

                    if (rootObject != null) openProjectMessage.ProjectView.Content = rootObject;
                }
            }
        }

        #endregion

        #region ctor
        
        public BuilderViewModel()
        {
            this.Project = ServiceLocator.Current.GetInstance<IProjectViewModel>();
            this.Toolbox = ServiceLocator.Current.GetInstance<IToolboxViewModel>();
            Properties = ServiceLocator.Current.GetInstance<IPropertiesViewModel>();
        }

        #endregion
    }
}
