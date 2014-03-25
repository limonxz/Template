using System;

namespace TemplateBuilder.Utility
{
    public class Utils
    {
        #region Singleton

        static Utils _Instance;
        static readonly object _lock = new Object();

        Utils()
        {
        }

        public static Utils Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new Utils();
                    }
                    return _Instance;
                }
            }
        }

        #endregion

        

        ///// <summary>
        ///// Event to get the first position and control selected
        ///// </summary>
        //void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    var ctrl = sender as Control;
        //    var ctrlPos = e.GetPosition(ctrl);
        ////    ItemSelected = ctrl;
        ////    ItemSelected.Focusable = true;
        ////    ItemSelected.Focus();
        //}

        ///// <summary>
        ///// Event to drag the control selected
        ///// </summary>
        //void MouseMove(object sender, MouseEventArgs e)
        //{
        //    var ctrl = sender as Control;

        //    if (e.LeftButton.Equals(MouseButtonState.Pressed)
        //        && ItemSelected.Equals(ctrl))
        //    {

        //        var ctrlPos = e.GetPosition(_Container);
        //        ctrl.SetValue(Canvas.LeftProperty, ctrlPos.X);
        //        ctrl.SetValue(Canvas.TopProperty, ctrlPos.Y);
        //    }
        //}
  
  
        //public static XmlTextWriter GetWriterForFolder(string fileName, Encoding encoding)
        //{
        //    FolderBrowserDialog dlg = new FolderBrowserDialog();
        //    if (dlg.ShowDialog() != DialogResult.OK)
        //        return null;

        //    XmlTextWriter writer = new XmlTextWriter(Path.Combine(dlg.SelectedPath, fileName), encoding);
        //    writer.Formatting = Formatting.Indented;

        //    return writer;
        //}

        //public static XmlTextWriter GetWriterForFile(Encoding encoding)
        //{
        //    SaveFileDialog dlg = new SaveFileDialog();
        //    dlg.Filter = "XML Files (*.xml)|*.xml";

        //    if (dlg.ShowDialog() != DialogResult.OK)
        //        return null;

        //    XmlTextWriter writer = new XmlTextWriter(dlg.FileName, encoding);
        //    writer.Formatting = Formatting.Indented;

        //    return writer;
        //}
    }
}
