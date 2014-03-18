using GalaSoft.MvvmLight.Messaging;
using TemplateBuilder.Views;

namespace TemplateBuilder.Model.Messages
{
    public class OpenProjectMessage : MessageBase
    {
        public string FilePath { get; set; }
        public ProjectView ProjectView { get; set; }
    }
}
