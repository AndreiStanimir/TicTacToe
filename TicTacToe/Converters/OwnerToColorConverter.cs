using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TicTacToe
{
    public class OwnerToColorConverter : IValueConverter
    {
        private readonly SolidColorBrush _player1Color = new SolidColorBrush(Colors.Blue);
        private readonly SolidColorBrush _player2Color = new SolidColorBrush(Colors.Red);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch { Owner.Player1 => _player1Color, Owner.Player2 => _player2Color, _=> null };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
