using System;
using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public class GameOverChecker
    {
        private readonly Board board;
        private readonly int minimumPiecesCount;

        public GameOverChecker(Board board, int minimumPiecesCount)
        {
            this.board = board;
            this.minimumPiecesCount = minimumPiecesCount;
            DiagonalCalculator = new DiagonalCalculator(board.Width, board.Height);
        }



        public DiagonalCalculator DiagonalCalculator { get; set; }

        public IEnumerable<WinningLine> WinningLines
        {
            get
            {
                var winningRows = new List<WinningLine>();

                winningRows.AddRange(GetWinningLines(board.Columns));
                winningRows.AddRange(GetWinningLines(board.Rows));
                winningRows.AddRange(GetWinningLines(board.Diagonals));

                return winningRows;
            }
        }

        public bool GetIsFull()
        {
            var squares = board.Squares;
            var areAllTaken = squares.All(square => square.Piece != null);
            return areAllTaken;
        }

        private static IEnumerable<WinningLine> GetWinningLines(IEnumerable<SquareList> squareCollection)
        {
            var winningLines = from column in squareCollection
                               where AreFullAndTakenBySamePlayer(column)
                               select new WinningLine
                               {
                                   Player = column.First().Piece.Player,
                                   Squares = column,
                               };

            return winningLines;
        }

        private static bool AreFullAndTakenBySamePlayer(IList<Square> squares)
        {
            var areSquaresFull = IsSquareCollectionFull(squares);
            if (areSquaresFull)
            {
                return AllTakenBySamePlayer(squares);
            }
            return false;
        }

        private static bool AllTakenBySamePlayer(IEnumerable<Square> row)
        {
            var allTakenBySamePlayer = row.Select(square => square.Piece.Player);
            var takenBySamePlayer = allTakenBySamePlayer.Distinct();
            return takenBySamePlayer.Count() == 1;
        }

        private static bool IsSquareCollectionFull(IEnumerable<Square> row)
        {
            return row.All(square => square.Piece != null);
        }

        public bool IsThisPositionEndingTheGame(Position position)
        {
            var row = board.Rows[position.Y];
            var column = board.Columns[position.X];
            var positiveDiagonal = GetPositiveDiagonal(position);
            var negativeDiagional = GetNegativeDiagonal(position);

            var rowCount = GetInlineCount(row, position.X);
            var columnCount = GetInlineCount(column, position.Y);
            var positiveDiagionalCount = GetInlineCount(positiveDiagonal, Math.Min(position.X, position.Y));
            var negativeDiagionalCount = GetInlineCount(negativeDiagional, Math.Min(position.X, board.Height - 1 - position.Y));

            var results = new List<int> { rowCount, columnCount, positiveDiagionalCount, negativeDiagionalCount };

            var isEndingPosition = results.Any(i => i >= minimumPiecesCount);

            return isEndingPosition;
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