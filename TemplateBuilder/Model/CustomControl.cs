using System.Windows.Controls;

namespace TemplateBuilder.Model
{
    public class CustomControl
    {
        public string Name { get; set; }

        /// <summary>
        /// The UI Control
        /// </summary>
        public Control TheControl { get; set; }
    }
}