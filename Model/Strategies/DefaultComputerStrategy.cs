using System.Linq;

namespace Model.Strategies
{
    public class DefaultComputerStrategy : IMoveStrategy
    {

        public Movement GetMoveFor(Board board, Player player)
        {
            var emptyPosition = GetFirstValidPosition(board);
            return new Movement(emptyPosition, player);
        }

        private static Position GetFirstValidPosition(Board board)
        {
            return board.GetValidMovePositions().First();            
        }
    }
}