using System.Collections.Generic;
using System.Linq;
using Model.Strategies.Minimax;

namespace Model
{
    public class BoardChecker
    {
        private readonly Board board;

        public BoardChecker(Board board)
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

        public bool HasWinningRow
        {
            get { return Has3InAnyRow || Has3InAnyColumn || Has3InAnyDiagonal; }
        }

        private bool Has3InAnyRow
        {
            get { return board.Rows.Any(AreFullAndTakenBySamePlayer); }
        }

        private bool Has3InAnyColumn
        {
            get { return board.Columns.Any(AreFullAndTakenBySamePlayer); }
        }

        private bool Has3InAnyDiagonal
        {
            get { return board.Diagonals.Any(AreFullAndTakenBySamePlayer); }
        }

        public IEnumerable<Player> GetPlayersWithLine()
        {
            var hashSet = new HashSet<Player>();
            hashSet.UnionWith(GetPlayersWithLine(board.Columns));
            hashSet.UnionWith(GetPlayersWithLine(board.Rows));
            hashSet.UnionWith(GetPlayersWithLine(board.Diagonals));
            return hashSet;
        }

        private IEnumerable<Player> GetPlayersWithLine(IEnumerable<SquareCollection> rows)
        {
            var rowsTakenBySamePlayer = from row in rows
                                        where AreFullAndTakenBySamePlayer(row)
                                        select row;

            var players = rowsTakenBySamePlayer.Select(GetFirstPlayer);
            return players;
        }

        private Player GetFirstPlayer(SquareCollection squares)
        {
            return squares.First().Piece.Player;
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

        public bool GetIsFull()
        {
            var squares = board.Squares;
            var areAllTaken = squares.All(square => square.Piece != null);
            return areAllTaken;
        }
    }
}