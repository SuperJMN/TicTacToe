using System.Collections.Generic;
using System.Linq;

namespace Model.Strategies.Minimax
{
    public class TicTacToeBoardEvaluator : IBoardEvaluator
    {
        public int Evaluate(Board board, Player player)
        {
            var rows = board.Rows.Sum(collection => Evaluate(collection, player));
            var columns = board.Columns.Sum(collection => Evaluate(collection, player));
            var diagonals = board.Diagonals.Sum(collection => Evaluate(collection, player));

            return rows + columns + diagonals;
        }

        private int Evaluate(SquareList list, Player player)
        {
            var takenByPlayerCount = GetTakenByPlayer(list, player).Count();
            var takenByOponentCount = GetTakenByOponent(list, player).Count();

            if (takenByPlayerCount == 3)
            {
                return 100;
            }

            if (takenByPlayerCount == 2 && takenByOponentCount == 0)
            {
                return 10;
            }

            if (takenByPlayerCount == 1 && takenByOponentCount == 0)
            {
                return 1;
            }

            if (takenByOponentCount == 3)
            {
                return -100;
            }
            if (takenByOponentCount == 2 && takenByPlayerCount == 0)
            {
                return -10;
            }

            if (takenByOponentCount == 1 && takenByPlayerCount == 0)
            {
                return -1;
            }

            return 0;
        }

        private static IEnumerable<Square> GetTakenByPlayer(IEnumerable<Square> collection, Player player)
        {
            return collection.Where(square => square.IsTakenBy(player));
        }

        private IEnumerable<Square> GetTakenByOponent(IEnumerable<Square> collection, Player player)
        {
            return collection.Where(square => IsTakenByOponent(square, player));
        }

        private bool IsTakenByOponent(Square square, Player player)
        {
            var piece = square.Piece;
            return piece != null && !piece.Player.Equals(player);
        }
    }
}