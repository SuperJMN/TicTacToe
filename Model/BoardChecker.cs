using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class BoardChecker
    {
        private Board board;

        public BoardChecker(Board board)
        {
            this.board = board;
        }

        public bool HasWinningRow
        {
            get { return GetHasWinningRow(); }
        }

        private bool GetHasWinningRow()
        {
            var v = CheckVerticals();
            var h = CheckHorizontals();
            var d = CheckDiagonals();

            return h || v || d;
        }

        private bool CheckHorizontals()
        {
            for (var i = 0; i < Board.BoardSize; i++)
            {
                var horizontalSlots = board.GetRowSlots(i).ToList();

                if (AreFullAndTakenBySamePlayer(horizontalSlots))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckDiagonals()
        {
            var diagonal1 = new List<Slot>();
            var diagonal2 = new List<Slot>();
            for (var i = 0; i < Board.BoardSize; i++)
            {
                var piece1 = board.GetSlot(new Position(i, i));
                diagonal1.Add(piece1);

                var piece2 = board.GetSlot(new Position(i, Board.BoardSize - i - 1));
                diagonal2.Add(piece2);
            }

            var checkDiagonal1 = AreFullAndTakenBySamePlayer(diagonal1);
            var checkDiagonal2 = AreFullAndTakenBySamePlayer(diagonal2);
           
            return checkDiagonal1 || checkDiagonal2;
        }

        private bool CheckVerticals()
        {
            for (var i = 0; i < Board.BoardSize; i++)
            {
                var verticalSlots = board.GetColumnSlots(i).ToList();
                if (AreFullAndTakenBySamePlayer(verticalSlots))
                {
                    return true;
                }
            }

            return false;
        }

      
        private static bool AreFullAndTakenBySamePlayer(List<Slot> slots)
        {
            var areSlotsFull = IsSlotCollectionFull(slots);
            if (areSlotsFull)
            {
                return AllTakenBySamePlayer(slots);
            }
            return false;
        }

        private static bool AllTakenBySamePlayer(IEnumerable<Slot> row)
        {
            var allTakenBySamePlayer = row.Select(slot => slot.Piece.Player);
            var takenBySamePlayer = allTakenBySamePlayer.Distinct();
            return takenBySamePlayer.Count() == 1;
        }

        private static bool IsSlotCollectionFull(IEnumerable<Slot> row)
        {
            return row.All(slot => slot.Piece != null);
        }

        public bool GetIsFull()
        {
            var allSlots = board.Slots.Cast<Slot>();
            var areAllTaken = allSlots.All(slot => slot.Piece != null);
            return areAllTaken;
        }
    }
}