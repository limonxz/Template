using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using TemplateBuilder.Model;

namespace TemplateBuilder.Views
{
    /// <summary>
    /// Interaction logic for BuilderView.xaml
    /// </summary>
    public partial class BuilderView : Window
    {
        /// <summary>
        /// Vars to move the object
        /// </summary>
        object _ControlObj;
        double _XPos, _YPos, _ArrowXPos, _ArrowYPos;

        /// <summary>
        /// ctor
        /// </summary>
        public BuilderView()
        {
            InitializeComponent();
            Messenger.Default.Register<CustomControl>(this, (dat) => ActionHandler(dat));
        }

        /// <summary>
        /// Get the item from other Listbox
        /// </summary>
        void ListBox_Drop(object sender, DragEventArgs e)
        {
            var parent = (ListBox)sender;
            var data = (CustomControl)e.Data.GetData(typeof(CustomControl));
            parent.Items.Add(data);

            Messenger.Default.Send<CustomControl>(data);
        }

        /// <summary>
        /// Action of Messenger (GalaSoft), to put the control inside the cambas
        /// </summary>
        void ActionHandler(CustomControl data)
        {
            if (data.TheControl != null)
            {
                data.TheControl.PreviewMouseLeftButtonDown += MouseLeftButtonDown;
                data.TheControl.PreviewMouseMove += MouseMove;
                data.TheControl.Cursor = Cursors.Hand;

                cContainer.Children.Add(data.TheControl);
            }
        }

        /// <summary>
        /// Event to get the first position and control selected
        /// </summary>
        void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _XPos = e.GetPosition(sender as Control).X;
            _YPos = e.GetPosition(sender as Control).Y;

            _ArrowXPos = e.GetPosition((sender as Control).Parent as Control).X - _XPos - 20;
            _ArrowYPos = e.GetPosition((sender as Control).Parent as Control).Y - _YPos - 20;

            _ControlObj = sender;
        }

        /// <summary>
        /// Event to drag the control selected
        /// </summary>
        void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                sender == _ControlObj)
            {
                var ctrl = (sender as Control);

                ctrl.SetValue(Canvas.LeftProperty, e.GetPosition((sender as Control).Parent as Control).X - _XPos - 20);
                ctrl.SetValue(Canvas.TopProperty, e.GetPosition((sender as Control).Parent as Control).Y - _YPos - 20);
            }
        }
    }
}
