using System;

namespace Model
{
    public class Square
    {
        private Piece piece;

        internal Square(Position position)
        {
            Position = position;
        }

        public Piece Piece
        {
            get { return piece; }
            internal set
            {
                piece = value;
                OnPieceChanged();
            }
        }

        public event EventHandler PieceChanged;

        private void OnPieceChanged()
        {
            var handler = PieceChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        public Position Position { get; private set; }

        public override string ToString()
        {
            if (Piece == null)
            {
                return Position +  " - Empty Square";
            } 

            return Position +  " - Taken by " + Piece.Player;
        }

        public bool IsTakenBy(Player player)
        {
            return Piece != null && Piece.Player.Equals(player);
        }

        public bool IsEmtpy
        {
            get { return Piece == null; }
        }
    }
}