using System;
using System.Linq;

namespace Model.Strategies
{
    public class RandomStrategy : IMoveStrategy
    {

        private readonly Random random;
        
        public RandomStrategy()
        {
            random = new Random((int) DateTime.Now.Ticks);
        }

        public Movement GetMoveFor(Board board, Player player)
        {
            var emptyPosition = GetRandomPosition(board);
            return new Movement(emptyPosition, player);
        }

        private Position GetRandomPosition(Board board)
        {
            var validPositions = board.GetValidMovePositions().ToList();

            var count = validPositions.Count;

            var randomEmptyIndex = random.Next(0, count - 1);

            return validPositions[randomEmptyIndex];
        }
    }
}