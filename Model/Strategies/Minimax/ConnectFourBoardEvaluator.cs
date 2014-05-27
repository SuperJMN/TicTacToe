using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Strategies.Minimax
{
    public class ConnectFourBoardEvaluator : IBoardEvaluator
    {
        public int Evaluate(Board board, Player player)
        {
            var validPositions = board.GetValidMovePositions();

            var scores = from position in validPositions
                         select new
                         {
                             Position = position,
                             Score = EvaluatePosition(board, player, position)
                         };

            var positionEvaluation = scores.Max(arg => arg.Score);

            return positionEvaluation;
        }

        private int EvaluatePosition(Board board, Player player, Position position)
        {
            var newBoard = board.Clone();
            newBoard.Move(new Movement(position, player));

            var rowScore = EvaluateRow(position, newBoard, player);
            var transposed = newBoard.Clone();
            transposed.Transpose();
            var columnScore = EvaluateColumn(position.Traspose(), transposed, player);
            
            return rowScore + columnScore;
        }

        private int EvaluateColumn(Position position, Board newBoard, Player player)
        {
            var squares = GetColumn(position, newBoard, 4);
            if (IsPossibleToFillWithPlayer(player, squares))
            {
                return 1;
            }
            return 0;
        }

        private static SquareList GetColumn(Position position, Board newBoard, int i)
        {
            var start = Math.Max(position.Y - i, 0);
            var end = Math.Min(position.Y + i - 1, newBoard.Height - 1);
            var count = end - start;

            var squareList = newBoard.Rows[position.X];

            var squares = squareList.GetRange(start, count);
            return new SquareList(squares);
        }

        private static int EvaluateRow(Position position, Board newBoard, Player player)
        {
            var squares = GetRow(position, newBoard, 4);
            if (IsPossibleToFillWithPlayer(player, squares))
            {
                return 1;
            }
            return 0;
        }

        private static bool IsPossibleToFillWithPlayer(Player player, IEnumerable<Square> squares)
        {
            return squares.All(square => square.IsEmtpy || square.IsTakenBy(player));
        }

        private static IEnumerable<Square> GetRow(Position position, Board newBoard, int i)
        {
            var start = Math.Max(position.X - i, 0);
            var end = Math.Min(position.X + i - 1, newBoard.Width - 1);
            var count = end - start;

            var squareList = newBoard.Rows[position.Y];

            var squares = squareList.GetRange(start, count);
            return new SquareList(squares);
        }
    }
}