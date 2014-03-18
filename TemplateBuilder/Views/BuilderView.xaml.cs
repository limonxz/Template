using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using TemplateBuilder.Model;
using TemplateBuilder.Model.Messages;

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
            Messenger.Default.Register<SaveProjectMessage>(this, SaveProject);
            Messenger.Default.Register<OpenProjectMessage>(this, OpenProject);
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
                

                ProjectView.ProjectTemplate.Children.Add(data.TheControl);

                //cContainer.Children.Add(data.TheControl);
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

        private void SaveProject(SaveProjectMessage msg)
        {
            var saveFileDialog = new SaveFileDialog();            
            saveFileDialog.ShowDialog();
            msg.FilePath = saveFileDialog.FileName;
            msg.ProjectView = ProjectView;
        }

        private void OpenProject(OpenProjectMessage msg)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            msg.FilePath = openFileDialog.FileName;
            msg.ProjectView = ProjectView;
        }
    }
}
