using System;
using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public class GameOverChecker
    {
        private Board board;
        private readonly int minimumPiecesCount;

        public GameOverChecker(Board board, int minimumPiecesCount)
        {
            Board = board;
            this.minimumPiecesCount = minimumPiecesCount;
            DiagonalCalculator = new DiagonalCalculator(board.Width, board.Height);
        }

        public DiagonalCalculator DiagonalCalculator { get; set; }

        public IEnumerable<WinningLine> WinningLines { get; set; }

        public Board Board
        {
            get { return board; }
            set
            {
                board = value;
                board.PlayerMoved += BoardOnPlayerMoved;
            }
        }

        public bool HasWinner { get; private set; }

        public bool IsGameOver
        {
            get { return Board.IsFull || HasWinner; }
        }

        private void BoardOnPlayerMoved(object sender, MovementEventArgs args)
        {
            var linesWithWinningInline = GetLinesWithWinningInline(args.Movement.Position);
            if (linesWithWinningInline.Any())
            {
                var winningLines = linesWithWinningInline.Select(list => new WinningLine()
                {
                    Squares = list,
                    Player = args.Movement.Player,
                });


                WinningLines = winningLines;
                HasWinner = true;
            }
        }

        private IEnumerable<SquareList> GetLinesWithWinningInline(Position position)
        {
            var list = new List<SquareList>();

            var row = new LineToBeExplored(board.Rows[position.Y], position.X);
            var column = new LineToBeExplored(board.Columns[position.X], position.Y);
            var diagonal1 = new LineToBeExplored(GetPositiveDiagonal(position), Math.Min(position.X, position.Y));
            var diagonal2 = new LineToBeExplored(GetNegativeDiagonal(position), Math.Min(position.X, board.Height - 1 - position.Y));

            AddIfWinner(row, list);
            AddIfWinner(column, list);
            AddIfWinner(diagonal1, list);
            AddIfWinner(diagonal2, list);

            return list;
        }

        private void AddIfWinner(LineToBeExplored lineToBeExplored, List<SquareList> list)
        {
            if (GetInlineCount(lineToBeExplored.SquareList, lineToBeExplored.X) >= minimumPiecesCount)
            {
                list.Add(lineToBeExplored.SquareList);
            }
        }

        private class LineToBeExplored
        {
            private readonly SquareList squareList;
            private readonly int x;

            public SquareList SquareList
            {
                get { return squareList; }
            }

            public int X
            {
                get { return x; }
            }

            public LineToBeExplored(SquareList squareList, int x)
            {
                this.squareList = squareList;
                this.x = x;
            }
        }

        public bool IsThisPositionEndingTheGame(Position position)
        {
            var linesWithWinningInline = GetLinesWithWinningInline(position);
            return linesWithWinningInline.Any();
        }

        private SquareList GetPositiveDiagonal(Position position)
        {
            var diagonalCalculator = new DiagonalCalculator(board.Width, board.Height);
            var positions = diagonalCalculator.GetDiagonalPositive(position);
            var squares = board.Squares.Where(square => positions.Contains(square.Position)).OrderBy(square => square.Position.X);
            return new SquareList(squares);
        }

        private SquareList GetNegativeDiagonal(Position position)
        {
            var diagonalCalculator = new DiagonalCalculator(board.Width, board.Height);
            var positions = diagonalCalculator.GetDiagonalNegative(position);
            var squares = board.Squares.Where(square => positions.Contains(square.Position)).OrderBy(square => square.Position.X);
            return new SquareList(squares);
        }

        private static int GetInlineCount(List<Square> squareCollection, int startIndex)
        {
            var rightPart = squareCollection.GetRange(startIndex, squareCollection.Count - startIndex);
            var leftPart = squareCollection.GetRange(0, startIndex + 1);
            leftPart.Reverse();

            var rightCount = GetRepeatCount(rightPart);
            var leftCount = GetRepeatCount(leftPart);

            return rightCount + leftCount - 1;
        }

        private static int GetRepeatCount(IReadOnlyList<Square> squareCollection)
        {
            var foundOther = false;
            var toFind = squareCollection.First().Piece.Player;
            var i = 1;
            var count = 1;
            while (i < squareCollection.Count && !foundOther)
            {
                var currentPiece = squareCollection[i].Piece;

                if (currentPiece == null)
                {
                    foundOther = true;
                }
                else
                {
                    var player = currentPiece.Player;
                    if (player == toFind)
                    {
                        count++;
                        i++;
                    }
                    else
                    {
                        foundOther = true;
                    }
                }
            }
            return count;
        }
    }
}