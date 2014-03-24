using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

public static class ExtensionMethods
{
    /// <summary>
    /// Extention to clone the XAML controls
    /// </summary>
    public static T XamlClone<T>(this T original) where T : UIElement
    {
        if (original == null)
        {
            return null;
        }

        object clone;
        using (var stream = new MemoryStream())
        {
            XamlWriter.Save(original, stream);
            stream.Seek(0, SeekOrigin.Begin);
            clone = XamlReader.Load(stream);
        }

        return clone is T ? (T)clone : null;
    }
}
