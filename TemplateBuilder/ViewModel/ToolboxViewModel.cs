using System.Collections.ObjectModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using TemplateBuilder.Model;
using TemplateBuilder.ViewModel.Interfaces;

namespace TemplateBuilder.ViewModel
{
    public class ToolboxViewModel : ViewModelBase, IToolboxViewModel
    {
        private CustomControl _selectedControl;
        ObservableCollection<CustomControl> _controls;

        public CustomControl SelectedControl
        {
            get { return _selectedControl; }
            set
            {
                _selectedControl = value;
                RaisePropertyChanged(() => this.SelectedControl);
            }
        }

        public ObservableCollection<CustomControl> Controls
        {
            get { return _controls; }
            set
            {
                _controls = value;
                RaisePropertyChanged(() => this.Controls);
            }
        }

        public ToolboxViewModel()
        {
            /// Data Dumy
            var controls = new Collection<CustomControl>
            {
                new CustomControl{Name = "TextBox", TheControl = new TextBox(){ Text = "It's TextBox", Width = 80}},
                new CustomControl{Name = "RadioButton", TheControl = new RadioButton() { Content = "I's RadioButton" }},
                new CustomControl{Name = "CheckBox", TheControl = new CheckBox() { Content = "I's CheckBox" }},
                new CustomControl{Name = "Control 4"},
                new CustomControl{Name = "Control 5"}
            };

            this.Controls = new ObservableCollection<CustomControl>(controls);
        }
    }
}
