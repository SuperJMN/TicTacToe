using System.Collections.Generic;
using System.Linq;

namespace Model.Utils
{
    public class PlayerPieceMapping : Dictionary<Player, char>
    {
        public PlayerPieceMapping()
        {
            
        }
        public PlayerPieceMapping(Board board)
        {
            var allPlayers = board.Squares.Where(square => square.Piece != null).Select(square => square.Piece.Player);
            var distinct = allPlayers.Distinct().ToList();

            var count = distinct.Count;
            if (count > 0)
            {
                Add(distinct[0], 'X');
                
            } 
            if (count > 1)
            {
                Add(distinct[1], 'O');
            }
        }
    }
}