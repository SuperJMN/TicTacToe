using System.Windows.Input;
using Cinch;
using Model;

namespace WPFTicTacToe
{
    public class PieceViewModel : ViewModelBase
    {
        private char pieceChar;
        private Piece Piece { get; set; }

        public char PieceChar
        {
            get { return pieceChar; }
            set
            {
                pieceChar = value;
                NotifyPropertyChanged("PieceChar");
            }
        }

        public ICommand MoveHumanPlayerCommand
        {
            get { throw new System.NotImplementedException(); }
        }

        public PieceViewModel(Piece piece, char pieceChar)
        {
            Piece = piece;
            PieceChar = pieceChar;
        }
    }
}