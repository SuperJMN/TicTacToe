using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ModelTest
{
    [TestClass]
    public class BoardEvaluationTests
    {

        [TestMethod]
        public void EvaluateEmpty()
        {
            var board = new Board();
            var player = new HumanPlayer("Name");
            var boardEvaluator = new BoardEvaluator(board);
            var value = boardEvaluator.Evaluate(player);
            Assert.AreEqual(0, value);
        }
    }

    public class BoardEvaluator
    {
        private Board Board { get; set; }

        public BoardEvaluator(Board board)
        {
            Board = board;
        }

        public int Evaluate(Player player)
        {
            var value = 0;

            for (int i = 0; i < Board.BoardSize; i++)
            {
                var row = Board.GetRowSlots(i);
                value += Evaluate(row, player);
            }

            return value;
        }

        private int Evaluate(IEnumerable<Slot> slots, Player player)
        {
            var count = slots.Count(slot => slot.Piece.Player.Equals(player));
            switch (count)
            {
                case 0:
                    return 1;
                case 1:
                    return 10;
                case 2:
                    return 100;
                case 3:
                    return 1000;
                default :
                    throw new IndexOutOfRangeException();
            }
        }
    }
}