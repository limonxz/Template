using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
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
            Messenger.Default.Register<SaveProjectMessage>(this, SaveProject);
            Messenger.Default.Register<OpenProjectMessage>(this, OpenProject);
        }

        private void SaveProject(SaveProjectMessage msg)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog();
            msg.FilePath = saveFileDialog.FileName;
            msg.ProjectView = pvContainer;
        }

        private void OpenProject(OpenProjectMessage msg)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            msg.FilePath = openFileDialog.FileName;
            msg.ProjectView = pvContainer;
        }
    }
}
