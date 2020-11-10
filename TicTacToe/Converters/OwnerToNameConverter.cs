using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TicTacToe
{
    public class OwnerToNameConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => (values[0] is Owner o && o==Owner.Player1)? values[1] : "Computer";

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}