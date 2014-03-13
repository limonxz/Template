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
            var controls = new Collection<CustomControl>
            {
                new CustomControl{Name = "Control 1"},
                new CustomControl{Name = "Control 2"},
                new CustomControl{Name = "Control 3"},
                new CustomControl{Name = "Control 4"},
                new CustomControl{Name = "Control 5"}
            };

            this.Controls = new ObservableCollection<CustomControl>(controls);
        }
    }
}
