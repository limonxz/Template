using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using TemplateBuilder.Model;

namespace TemplateBuilder.Views
{
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the item from other Listbox
        /// </summary>
        void ListBox_Drop(object sender, DragEventArgs e)
        {
            //var parent = (StackPanel)sender;
            var data = (CustomControl)e.Data.GetData(typeof(CustomControl));
            //parent.Children.Add(data.TheControl);
            //parent.Items.Add(data);

            Messenger.Default.Send<CustomControl>(data);
        }
    }
}
