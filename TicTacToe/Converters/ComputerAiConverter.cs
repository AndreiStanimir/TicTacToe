using System;
using System.Globalization;
using System.Windows.Data;

namespace TicTacToe
{
    public class ComputerAiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (value is bool v && v)?"Computer is smart": "Computer is stupid";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}