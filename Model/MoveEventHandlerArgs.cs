using System;

namespace Model
{
    public class MoveEventHandlerArgs : EventArgs
    {
        public MoveEventHandlerArgs(Movement movement)
        {
            Movement = movement;
        }

        public Movement Movement { get; set; }
    }
}