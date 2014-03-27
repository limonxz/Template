using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using TemplateBuilder.Model;
using TemplateBuilder.Model.Messages;
using TemplateBuilder.ViewModel.Interfaces;
using TemplateBuilder.Views;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace TemplateBuilder.ViewModel
{
    public class EditableCanvasViewModel : ViewModelBase, IEditableCanvasViewModel
    {
        public EditableCanvasViewModel()
        {
            IsVisible = Visibility.Hidden;
        }

        private Visibility _isVisible;

        public Visibility IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        public UIElement Source { get; set; }

        private Point _position;
        public Point Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                RaisePropertyChanged(() => this.Position);
            }
        }
    }
}
