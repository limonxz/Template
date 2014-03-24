using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace TemplateBuilder.Model.Messages
{
    public class ControlMessage : MessageBase
    {
        /// <summary>
        /// To manage the control, with the properties and project
        /// </summary>
        public Control TheControl { get; set; }
    }
}
