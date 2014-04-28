using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class BoardChecker
    {
        private readonly Board board;

        public BoardChecker(Board board)
        {
            this.board = board;
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

        private static bool AreFullAndTakenBySamePlayer(IList<Square> slots)
        {
            var areSlotsFull = IsSlotCollectionFull(slots);
            if (areSlotsFull)
            {
                return AllTakenBySamePlayer(slots);
            }
            return false;
        }

        private static bool AllTakenBySamePlayer(IEnumerable<Square> row)
        {
            var allTakenBySamePlayer = row.Select(slot => slot.Piece.Player);
            var takenBySamePlayer = allTakenBySamePlayer.Distinct();
            return takenBySamePlayer.Count() == 1;
        }

        private static bool IsSlotCollectionFull(IEnumerable<Square> row)
        {
            return row.All(slot => slot.Piece != null);
        }

        public bool GetIsFull()
        {
            var squares = board.Squares;
            var areAllTaken = squares.All(slot => slot.Piece != null);
            return areAllTaken;
        }
    }
}