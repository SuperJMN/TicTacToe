using System;

namespace Model.Strategies
{
    public class DefaultComputerStrategy : IMoveStrategy
    {
        public Move GetMoveFor(Board board)
        {
            var emptyPosition = GetEmptyPosition(board);
            return new Move(emptyPosition);
        }

        private Position GetEmptyPosition(Board board)
        {
            Square square = null;

            for (int i = 0; i < Board.BoardSize; i++)
            {
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    var position = new Position(i, j);
                    if (board.GetPiece(position) == null)
                    {
                        return position;
                    }
                }
            }

            throw new InvalidOperationException("Cannot retrieve an empty Square");
        }
    }
}