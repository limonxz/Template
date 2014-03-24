using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using TemplateBuilder.Views;

namespace TemplateBuilder.Model.Messages
{
    public class OpenProjectMessage : MessageBase
    {
        /// <summary>
        /// File path of the template 
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Project View Template
        /// </summary>
        public ProjectView ProjectViewTemplate { get; set; }

        /// <summary>
        /// For get the Current Container (Panel)
        /// </summary>
        public Panel CurrrentContainer { get; set; }
    }
}
