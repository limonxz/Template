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
            
            var result = saveFileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                using (var fs = new FileStream(saveFileDialog.FileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    XamlWriter.Save(pvContainer.ProjectTemplate, fs);
                }

                pvContainer.ProjectTemplate.Items.Clear();
            }
        }

        private void OpenProject(OpenProjectMessage msg)
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                using (var mysr = new StreamReader(openFileDialog.FileName))
                {
                    var rootObject = XamlReader.Load(mysr.BaseStream) as StackPanel;

                    if (rootObject != null) pvContainer.Content = rootObject;
                }
            }
        }
    }
}
