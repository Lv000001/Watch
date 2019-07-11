using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Watch.Element;

namespace Watch
{

    public class VisibilityToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Visibility)value == Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            return ((bool)value? Visibility.Visible: Visibility.Collapsed);
        }
    }

    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HWBox box)
            {
                return new SolidColorBrush(Color.FromRgb(box.Color_red, box.Color_green, box.Color_blue));
            }
            if (value is HWTextareaWithOneWildCard text)
            {
                return new SolidColorBrush(Color.FromArgb(text.Alpha, text.Color_red, text.Color_green, text.Color_blue));
            }

            if (value is HWTextareaWithTwoWildCard textarea)
            {
                return new SolidColorBrush(Color.FromArgb(textarea.Alpha, textarea.Color_red, textarea.Color_green, textarea.Color_blue));
            }

            return new SolidColorBrush(Colors.Transparent); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value ? Visibility.Visible : Visibility.Collapsed);
        }
    }

}
