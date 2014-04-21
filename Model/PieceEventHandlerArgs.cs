using System;

namespace Model
{
    public class PieceEventHandlerArgs : EventArgs
    {
        public PieceEventHandlerArgs(Piece piece)
        {
            Piece = piece;
        }

        public Piece Piece { get; set; }
    }
}