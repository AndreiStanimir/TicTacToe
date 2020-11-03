using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TicTacToe
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (value is bool v && v) ? Visibility.Visible : (parameter is string p && p == "hidden") ? Visibility.Hidden : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
