using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace VanDerWaerdenGame.DesktopApp
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts boolean Value to 
        /// </summary>
        /// <param name="value">Boolean value to be converted.</param>
        /// <param name="parameter">If parameter is false, then converted value will be negated before converting.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value is bool)
            {
                var v = (bool)value;
                if ( (parameter is bool && (bool)parameter == false) || 
                    (parameter is string && !Boolean.Parse(parameter as string)) )
                    v = !v;
                return (v) ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new ArgumentException("Value must be a boolean value.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            Visibility visibility;
            if (Enum.TryParse<Visibility>(value.ToString(), out visibility))
            {
                bool v;
                if (visibility == Visibility.Visible)
                    v = true;
                else
                    v = false;
                if (parameter is bool && (bool)parameter == false)
                    return !v;
                return v;
            }
            throw new ArgumentException("Value must be Collapsed or Visible.");
        }
    }
}
