using System;
using System.Linq;

namespace Model.Strategies
{
    public class DefaultComputerStrategy : IMoveStrategy
    {

        public Movement GetMoveFor(Board board, Player player)
        {
            var emptyPosition = GetEmptyPosition(board);
            return new Movement(emptyPosition, player);
        }

        private Position GetEmptyPosition(Board board)
        {
            return board.GetEmptyPositions().First();            
        }
    }
}