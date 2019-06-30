using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace HR_Portal_Test.Source.Utility
{
    public class ViewTools
    {
        public static void SearchInputLostFocus(TextBox textbox, TextBox textboxSecondary )
        {
      
            if (textbox.Text == "")
            {
                textboxSecondary.Visibility = Visibility.Visible;
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["racs_light"];
            }
            else
            {
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["ThemeColor"];
                textbox.Foreground = Brushes.Black;
            }
        }
        public static void SearchInputGotFocus(TextBox textbox, TextBox textboxSecondary)
        {
            if (textbox.Text == "")
                textboxSecondary.Visibility = Visibility.Hidden;
        }

        public static int TextBoxLength(TextBox textbox)
        {
            return textbox.Text.Length;
        }
    }
}
