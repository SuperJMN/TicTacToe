using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Strategies.Minimax
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
            var rows = Board.Rows.Sum(collection => Evaluate(collection, player));
            var columns = Board.Columns.Sum(collection => Evaluate(collection, player));
            var diagonals = Board.Diagonals.Sum(collection => Evaluate(collection, player));

            return rows + columns + diagonals;
        }

        private int Evaluate(SquareCollection collection, Player player)
        {
            var takenByPlayerCount = TakenByPlayer(collection, player).Count();
            var takenByOponentCount = TakenByOponent(collection, player).Count();

            if (takenByPlayerCount == 3)
            {
                return 100;
            }
            if (takenByPlayerCount == 2 && takenByOponentCount == 0)
            {
                return 10;
            }

            if (takenByPlayerCount == 1 && takenByOponentCount == 0)
            {
                return 1;
            }

            if (takenByOponentCount == 3)
            {
                return -100;
            }
            if (takenByOponentCount == 2 && takenByPlayerCount == 0)
            {
                return -10;
            }

            if (takenByOponentCount == 1 && takenByPlayerCount == 0)
            {
                return -1;
            }

            return 0;
        }

        private static int Evaluate(int takenByPlayer)
        {
            switch (takenByPlayer)
            {
                case 0:
                    return 0;
                case 1:
                    return 10;
                case 2:
                    return 100;
                case 3:
                    return 1000;

                default:
                    throw new InvalidOperationException();
            }
        }

        private IEnumerable<Square> TakenByPlayer(IEnumerable<Square> collection, Player player)
        {
            return collection.Where(square => IsTakenBy(square, player));
        }

        private IEnumerable<Square> TakenByOponent(IEnumerable<Square> collection, Player player)
        {
            return collection.Where(square => IsTakenByOponent(square, player));
        }

        private static bool IsTakenBy(Square square, Player player)
        {
            var piece = square.Piece;
            return piece != null && piece.Player.Equals(player);
        }

        private bool IsTakenByOponent(Square square, Player player)
        {
            var piece = square.Piece;
            return piece != null && !piece.Player.Equals(player);
        }
    }
}