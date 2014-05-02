using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFTicTacToe.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class Bool2VisibilityHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = (bool)value;
            return converted ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = (Visibility)value;
            return converted == Visibility.Hidden;
        }
    }
}