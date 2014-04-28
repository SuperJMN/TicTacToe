using System;

namespace Model
{
    public class Movement
    {
        public Position Position { get; private set; }
        public Player Player { get; set; }

        public Movement(Position position, Player player)
        {
            Player = player;
            Position = position;
        }

        public override string ToString()
        {
            return "Movement to " + Position;
        }
    }
}