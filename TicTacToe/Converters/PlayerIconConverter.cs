using System;
using System.Globalization;
using System.Windows.Data;

namespace TicTacToe
{
    public class PlayerIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch { Owner.Player1 => "X", Owner.Player2 => "O", _ => "" };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}