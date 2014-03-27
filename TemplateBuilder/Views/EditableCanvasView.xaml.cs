using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TemplateBuilder.Model.Messages;
using TemplateBuilder.ViewModel;

namespace TemplateBuilder.Views
{
    /// <summary>
    /// Interaction logic for EditableCanvasView.xaml
    /// </summary>
    public partial class EditableCanvasView : UserControl
    {
        public IEditableCanvasViewModel ViewModel { get; set; }

        public EditableCanvasView()
        {
            InitializeComponent();
        }

        public EditableCanvasView(IEditableCanvasViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
            DataContext = this.ViewModel;

            SetFocusScope();
            SetCanvasPosition();
        }

        private void SetCanvasPosition()
        {
            Canvas.SetTop(FloatingTextBox, ViewModel.Position.Y);
            Canvas.SetLeft(FloatingTextBox, ViewModel.Position.X);

            //if (_floatingTextBlock is TextBlock)
            //    FloatingTextBox.Text = ((TextBlock)_floatingTextBlock).Text;

            //Control control = VisualTreeHelper.GetParent(ctrl) as Control;
            //Control control = (Control)e.Source;

            if (ViewModel.Source is TextBlock)
                FloatingTextBox.Width = ((TextBlock)ViewModel.Source).ActualWidth;
            ViewModel.IsVisible = Visibility.Visible;
            FloatingTextBox.Focus();
            FloatingTextBox.SelectAll();
        }

        private TextBlock _floatingTextBlock;

        //private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ClickCount < 2) return;

        //    _floatingTextBlock = e.OriginalSource as TextBlock;
        //    if (_floatingTextBlock == null) return;

        //    Point relativePoint = _floatingTextBlock.TransformToAncestor()
        //        .Transform(new Point(0, 0));

        //    Canvas.SetTop(FloatingTextBox, relativePoint.Y);
        //    Canvas.SetLeft(FloatingTextBox, relativePoint.X);

        //    if (_floatingTextBlock is TextBlock)
        //        FloatingTextBox.Text = ((TextBlock)_floatingTextBlock).Text;

        //    //Control control = VisualTreeHelper.GetParent(ctrl) as Control;
        //    //Control control = (Control)e.Source;

        //    FloatingTextBox.Width = Source.ActualWidth;
        //    FloatingCanvas.Visibility = Visibility.Visible;
        //    FloatingTextBox.Focus();
        //    FloatingTextBox.SelectAll();
        //}

        private void SetFocusScope()
        {
            DependencyObject scope = FocusManager.GetFocusScope(FloatingTextBox);
            FocusManager.SetFocusedElement(scope, FloatingCanvas as IInputElement);
        }

        private void FloatingTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.IsVisible = Visibility.Hidden;
            var finishMessage = new FinishEditingMessage(FloatingTextBox.Text);
            Messenger.Default.Send(finishMessage);
        }
    }
}
