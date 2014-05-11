namespace Model
{
    public abstract class Player
    {
        protected Player(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract void RequestMove(Board board);

        public override string ToString()
        {
            return this.Name;
        }

        protected bool Equals(Player other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Player)obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Player left, Player right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Player left, Player right)
        {
            return !Equals(left, right);
        }

        public void MakeMove(Position position)
        {
            OnWantToMove(new PositionEventHandlerArgs(position));
        }

        public event PositionEventHandler WantToMove;

        protected void OnWantToMove(PositionEventHandlerArgs e)
        {
            var handler = WantToMove;
            if (handler != null) handler(this, e);
        }
    }
}