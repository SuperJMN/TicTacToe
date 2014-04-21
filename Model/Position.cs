using System;

namespace Model
{
    public struct Position
    {
        public Position(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return String.Format("{{{0},{1}}}", X, Y);
        }
    }
}