using System;

namespace Model
{
    public class MovementEventArgs : EventArgs
    {
        public Movement Movement { get; private set; }

        public MovementEventArgs(Movement movement)
        {
            Movement = movement;
        }
    }
}