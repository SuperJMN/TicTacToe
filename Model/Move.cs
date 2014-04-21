using System;

namespace Model
{
    public class Move
    {
        public Position Position { get; private set; }

        public Move(Position position)
        {
            Position = position;            
        }
    }
}