using System;

namespace Model
{
    public class Square
    {
        private Piece piece;

        public Square(Position position)
        {
            Position = position;
        }

        public Piece Piece
        {
            get { return piece; }
            set
            {
                piece = value;
                OnPieceChanged();
            }
        }

        public event EventHandler PieceChanged;

        protected virtual void OnPieceChanged()
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
    }
}