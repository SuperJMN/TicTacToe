using System;

namespace Model
{
    public class PositionEventHandlerArgs : EventArgs
    {
        public PositionEventHandlerArgs(Position position)
        {
            Position = position;
        }

        public Position Position { get; private set; }
    }
}