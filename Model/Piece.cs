using System.Linq.Expressions;

namespace Model
{
    public class Piece
    {

        public Piece(Player player)
        {
            Player = player;
        }

        public Player Player { get; set; }

        public Piece Clone()
        {
            return new Piece(Player);
        }
    }
}