using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VanDerWaerdenGame.DesktopApp
{
    public class BoardToElipsesConverter : IValueConverter
    {
        private static List<SolidColorBrush> Colors = new List<SolidColorBrush> {Brushes.Blue, Brushes.Red, Brushes.LightGoldenrodYellow, Brushes.Olive};

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorIndex = (int)value;
            return (colorIndex != -1) ? Colors[colorIndex] : Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
