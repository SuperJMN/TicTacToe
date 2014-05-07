using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class GameOverChecker
    {
        private readonly Board board;

        public GameOverChecker(Board board)
        {
            this.board = board;
        }

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

        private static IEnumerable<WinningLine> GetWinningLines(IEnumerable<SquareCollection> squareCollection)
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
    }
}