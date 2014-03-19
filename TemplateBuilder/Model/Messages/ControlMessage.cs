using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace TemplateBuilder.Model.Messages
{
    public class ControlMessage : MessageBase
    {
        public Control TheControl { get; set; }
    }
}
