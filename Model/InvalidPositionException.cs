using System;

namespace Model
{
    public class InvalidPositionException : Exception
    {
        public Position Position { get; private set; }

        public InvalidPositionException(Position position)
        {
            Position = position;
        }

        public override string Message
        {
            get { return "Invalid position " + Position; }
        }
    }
}