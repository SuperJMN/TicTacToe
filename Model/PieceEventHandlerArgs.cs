using System;

namespace Model
{
    public class PieceEventHandlerArgs : EventArgs
    {
        public PieceEventHandlerArgs(Piece piece, Position position)
        {
            Piece = piece;
            Position = position;
        }

        public Piece Piece { get; private set; }
        public Position Position { get; private set; }


    }
}