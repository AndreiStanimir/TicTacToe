using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TicTacToe
{
    public class OwnerToColorConverter : IValueConverter
    {
        private readonly SolidColorBrush _player1Color = new SolidColorBrush(Colors.Green);
        private readonly SolidColorBrush _player2Color = new SolidColorBrush(Colors.Black);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch { Owner.Player1 => _player1Color, Owner.Computer => _player2Color, _ => null };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}