namespace Model
{
    public abstract class Player
    {
        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

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

        public void MakeMove(Move move)
        {
            OnWantToMove(new MoveEventHandlerArgs(move));
        }

        public virtual event MoveEventHandler WantToMove;

        protected virtual void OnWantToMove(MoveEventHandlerArgs e)
        {
            var handler = WantToMove;
            if (handler != null) handler(this, e);
        }
    }
}