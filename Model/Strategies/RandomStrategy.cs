using System;
using System.Collections.Generic;
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
            var emptyPositions = board.GetEmptyPositions().ToList();

            var count = emptyPositions.Count();

            var randomEmptyIndex = random.Next(0, count - 1);

            return emptyPositions[randomEmptyIndex];
        }
    }
}