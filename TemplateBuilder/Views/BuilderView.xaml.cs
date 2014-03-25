using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using TemplateBuilder.Model.Messages;

namespace TemplateBuilder.Views
{
    /// <summary>
    /// Interaction logic for BuilderView.xaml
    /// </summary>
    public partial class BuilderView : Window
    {
        /// <summary>
        /// ctor
        /// </summary>
        public BuilderView()
        {
            InitializeComponent();
            
            /// For set the Project view control, into the VM's
            var projectContainerMessage = new ProjectContainerMessage();
            projectContainerMessage.ProjectViewContainer = pvContainer;
            Messenger.Default.Send(projectContainerMessage);
        }
    }
}
