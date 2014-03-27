using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace TemplateBuilder.Model.Messages
{
    public class FinishEditingMessage : MessageBase
    {
        public string Text { get; set; }

        public FinishEditingMessage(string text)
        {
            Text = text;
        }

    }
}