using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TemplateBuilder.ViewModel
{
    public interface IEditableCanvasViewModel
    {
        Visibility IsVisible { get; set; }
        UIElement Source { get; set; }
        Point Position { get; set; }
    }
}
