using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Strategies
{

    public class BoardEvaluator
    {
        private Board Board { get; set; }

        public BoardEvaluator(Board board)
        {
            Board = board;
        }

        public int Evaluate(Player player)
        {
            var rows = Board.Rows.Sum(square => Evaluate(square, player));
            var columns = Board.Columns.Sum(square => Evaluate(square, player));
            var diagonals = Board.Diagonals.Sum(square => Evaluate(square, player));
            return rows + columns + diagonals;
        }

        private int Evaluate(IList<Square> slots, Player player)
        {
            if (AreFreeOrTakenBy(slots, player))
            {
                var slotsCount = slots.Count(slot => IsTakenByPlayer(slot, player));
                return GetPoints(slotsCount);
            }

            return 0;
        }

        private static int GetPoints(int count)
        {
            var pow = Math.Pow(10, count);
            return (int) pow;
        }

        private static bool IsTakenByPlayer(Square square, Player toMatch)
        {
            var piece = square.Piece;
            if (piece == null)
            {
                return false;
            }

            return (piece.Player.Equals(toMatch));
        }

        private static bool AreFreeOrTakenBy(IEnumerable<Square> slots, Player player)
        {
            return slots.All(slot => slot.Piece == null || slot.Piece.Player.Equals(player));
        }


    }
}