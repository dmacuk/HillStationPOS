using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HillStationPOS.Convertors
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var header = (string) value;
            return header == "Set Meal" ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}