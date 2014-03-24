using GalaSoft.MvvmLight.Messaging;
using TemplateBuilder.Views;

namespace TemplateBuilder.Model.Messages
{
    public class SaveProjectMessage : MessageBase
    {
        /// <summary>
        /// File path of the template
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The Project View Template
        /// </summary>
        public ProjectView ProjectViewTemplate { get; set; }
    }
}
