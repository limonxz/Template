using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TemplateBuilder.Model;
using TemplateBuilder.ViewModel.Interfaces;

namespace TemplateBuilder.ViewModel
{
    public class ToolboxViewModel : ViewModelBase, IToolboxViewModel
    {
        #region Properties

        ObservableCollection<CustomControl> _controls;
        public ObservableCollection<CustomControl> Controls
        {
            get { return _controls; }
            set
            {
                _controls = value;
                RaisePropertyChanged(() => this.Controls);
            }
        }

        #endregion

        #region Commands

        internal ICommand _ListBoxPreviewMouseLeftButtonDown;
        /// <summary>
        /// Use it for drag the control selected
        /// </summary>
        public ICommand ListBoxPreviewMouseLeftButtonDown
        {
            get
            {
                if (_ListBoxPreviewMouseLeftButtonDown == null)
                {
                    _ListBoxPreviewMouseLeftButtonDown = new RelayCommand<MouseButtonEventArgs>(Execute_ListBoxPreviewMouseLeftButtonDown);
                }

                return _ListBoxPreviewMouseLeftButtonDown;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Command Action: ListBoxPreviewMouseLeftButtonDown
        /// Use for drag the control selected
        /// </summary>
        void Execute_ListBoxPreviewMouseLeftButtonDown(MouseButtonEventArgs _object)
        {
            var parent = (ListBox)_object.Source;
            var dragSource = parent;

            var data = GetDataFromListBox(dragSource, _object.GetPosition(parent));

            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Copy);
            }
        }

        #endregion

        #region ctor

        public ToolboxViewModel()
        {
            var controls = FillListControls();

            this.Controls = new ObservableCollection<CustomControl>(controls);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// For fill the list with data dumy
        /// </summary>
        /// <returns></returns>
        Collection<CustomControl> FillListControls()
        {
            var controls = new Collection<CustomControl>
            {
                new CustomControl{Name = "TextBox", TheControl = new TextBox(){ Text = "It's TextBox", Width = 80}},
                new CustomControl{Name = "RadioButton", TheControl = new RadioButton() { Content = "I's RadioButton" }},
                new CustomControl{Name = "CheckBox", TheControl = new CheckBox() { Content = "I's CheckBox" }},
                new CustomControl{Name = "DatePicker", TheControl = new DatePicker() },
                new CustomControl{Name = "Label", TheControl = new Label() { Content="It's Label" } }
            };

            return controls;
        }

        /// <summary>
        /// Get Data of ListBox
        /// </summary>
        CustomControl GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                var data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return (CustomControl)data;
                }
            }
            return null;
        }

        #endregion
    }
}
