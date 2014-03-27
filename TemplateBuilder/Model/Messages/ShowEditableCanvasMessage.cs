using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace TemplateBuilder.Model.Messages
{
    public class ShowEditableCanvasMessage : MessageBase
    {
        public UIElement Control { get; set; }
        public Point Position { get; set; }

        public ShowEditableCanvasMessage(UIElement control, Point position)
        {
            Control = control;
            Position = position;
        }

    }
}