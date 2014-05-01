using System;
using System.Globalization;
using System.Windows.Data;
using Model;

namespace WPFTicTacToe
{
    public class PlayerToPieceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var player = (Player)value;
            if (player.Name == "JMN")
            {
                return Pieces.O;
            }
            else
            {
                return Pieces.X;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}