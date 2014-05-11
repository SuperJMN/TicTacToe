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

        public int X { get; private set; }
        public int Y { get; private set; }

        public override string ToString()
        {
            return String.Format("{{{0},{1}}}", X, Y);
        }

        public bool Equals(Position other)
        {
            return Y == other.Y && X == other.X;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Position && Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Y*397) ^ X;
            }
        }

        public static bool operator ==(Position x, Position y)
        {
            return x.Equals(y);
        }
        public static bool operator !=(Position x, Position y)
        {
            return !x.Equals(y);
        }

    }
}