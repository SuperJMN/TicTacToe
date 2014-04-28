namespace Model
{
    public class Square
    {
        public Square(Position position)
        {
            Position = position;
        }
        public Piece Piece { get; set; }
        public Position Position { get; private set; }

        public override string ToString()
        {
            if (Piece == null)
            {
                return Position +  " - Empty Square";
            } 

            return Position +  "Taken by " + Piece.Player;
        }       
    }
}