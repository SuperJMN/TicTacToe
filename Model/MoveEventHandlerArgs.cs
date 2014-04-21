using System;

namespace Model
{
    public class MoveEventHandlerArgs : EventArgs
    {
        public MoveEventHandlerArgs(Move move)
        {
            Move = move;
        }

        public Move Move { get; set; }
    }
}