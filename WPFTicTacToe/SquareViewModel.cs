using System;
using Cinch;
using Model;
using Model.Utils;

namespace WPFTicTacToe
{
    public class SquareViewModel : ViewModelBase
    {
        private Square square;
        private PieceViewModel piece;
        private bool isWinning;

        private Square Square
        {
            get { return square; }
            set
            {
                square = value;                
                square.PieceChanged += SquareOnPieceChanged;
                if (square.Piece != null)
                {
                    Piece = new PieceViewModel(square.Piece, PlayerPieceMapping[square.Piece.Player]);
                }
            }
        }

        private void SquareOnPieceChanged(object sender, EventArgs eventArgs)
        {
            var piece = ((Square) sender).Piece;
            var player = piece.Player;
            Piece = new PieceViewModel(piece, PlayerPieceMapping[player]);
        }

        public PlayerPieceMapping PlayerPieceMapping { get; set; }

        public SquareViewModel(Square square, PlayerPieceMapping playerPieceMapping)
        {
            PlayerPieceMapping = playerPieceMapping;
            Square = square;           
        }

        public bool IsEmpty
        {
            get { return Square.Piece == null; }
        }

        public PieceViewModel Piece
        {
            get { return piece; }
            private set
            {
                piece = value;
                NotifyPropertyChanged("Piece");
            }
        }

        public Position Position
        {
            get { return Square.Position; }
        }

        public bool IsWinning
        {
            get { return isWinning; }
            set
            {
                isWinning = value;
                NotifyPropertyChanged("IsWinning");
            }
        }
    }
}