using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Strategies.Minimax
{
    public class ConnectFourBoardEvaluator : IBoardEvaluator
    {
        private readonly ConnectFourBoard board;

        public ConnectFourBoardEvaluator(ConnectFourBoard board)
        {
            this.board = board;
        }

        public int Evaluate(Player player)
        {
            var validPositions = board.GetValidMovePositions();

            var scores = from position in validPositions
                         select new
                         {
                             Position = position,
                             Score = EvaluatePosition(player, position)
                         };

            var positionEvaluation = scores.Max();

            return positionEvaluation.Score;
        }

        private int EvaluatePosition(Player player, Position position)
        {
            var newBoard = board.Clone();
            newBoard.Move(new Movement(position, player));

            int rowScore = EvaluateRow(position, newBoard);
            return rowScore;
        }

        private int EvaluateRow(Position position, Board newBoard)
        {
            var squares = GetRow(position, newBoard, 4);
            return 0;
        }

        private SquareList GetRow(Position position, Board newBoard, int i)
        {
            var start = Math.Max(position.X - i, 0);
            var end = Math.Min(position.X + i, newBoard.Width - 1);
            var count = board.Width - start - end;

            var squares = newBoard.Rows[position.Y].GetRange(start, count);
            return new SquareList(squares);
        }
    }
}