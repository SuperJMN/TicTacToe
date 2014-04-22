using System.Collections.Generic;
using System.Linq;

namespace Model.Strategies
{
    public class MinimaxStrategy : IMoveStrategy
    {
        
        public MinimaxStrategy(Board board)
        {
           BoardEvaluator = new BoardEvaluator(board);
        }

        private BoardEvaluator BoardEvaluator { get; set; }


        public Move GetMoveFor(Board board, Player player)
        {
            var emptySquares = GetEmptySquares(board);
            foreach (var emptySquare in emptySquares)
            {
                var boardClone = board.Clone();
            }

            return new Move(new Position(0, 0));
        }

        private static IEnumerable<Square> GetEmptySquares(Board board)
        {
            return board.Squares.Where(square => square.Piece == null);
        }
    }
}