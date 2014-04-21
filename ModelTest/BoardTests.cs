using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ModelTest
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void MakeValidMove()
        {
            var board = new Board();
            var position = new Position(1, 2);
            var move = new Move(position);
            var player = new HumanPlayer("Pepito");
            board.Move(player, move);

            var piece = board.GetPiece(position);
            Assert.AreEqual(player, piece.Player);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void MoveOutOfBoard()
        {
            var board = new Board();
            var position = new Position(6, 2);
            var move = new Move(position);
            var player = new HumanPlayer("Pepito");
            board.Move(player, move);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void MoveToSlotAlreadyTaken()
        {
            var board = new Board();
            var position = new Position(2, 2);
            var move = new Move(position);
            var player = new HumanPlayer("Pepito");
            board.Move(player, move);
            board.Move(player, move);
        }

        [TestMethod]
        public void WinGameHorizontal()
        {
            var board = new Board();
            var player = new HumanPlayer("Pepito");
            board.Move(player, new Move(new Position(0, 1)));
            board.Move(player, new Move(new Position(1, 1)));
            board.Move(player, new Move(new Position(2, 1)));

            Assert.IsTrue(board.BoardChecker.HasWinningRow);
        }

        [TestMethod]
        public void WinGameVertical()
        {
            var board = new Board();
            var player = new HumanPlayer("Pepito");
            board.Move(player, new Move(new Position(0, 0)));
            board.Move(player, new Move(new Position(0, 1)));
            board.Move(player, new Move(new Position(0, 2)));

            Assert.IsTrue(board.BoardChecker.HasWinningRow);
        }


        [TestMethod]
        public void FullRowWithBothPlayersShouldNotWin()
        {
            var board = new Board();
            var player1 = new HumanPlayer("Pepito");
            var player2 = new HumanPlayer("Juanito");
            board.Move(player1, new Move(new Position(0, 0)));
            board.Move(player2, new Move(new Position(0, 1)));
            board.Move(player1, new Move(new Position(0, 2)));

            Assert.IsFalse(board.BoardChecker.HasWinningRow);
        }

        [TestMethod]
        public void BoardIsIncomplete()
        {
            var board = new Board();
            Assert.IsFalse(board.IsFull);
        }

        [TestMethod]
        public void BoardIsFull()
        {
            var board = new Board();
            var player = new HumanPlayer("Pepito");
            FillBoard(player, board);
            Assert.IsTrue(board.IsFull);
        }

        private void FillBoard(Player player, Board board)
        {
            for (var i = 0; i < Board.BoardSize; i++)
            {
                for (var j = 0; j < Board.BoardSize; j++)
                {
                    board.Move(player, new Move(new Position(i, j)));
                }
            }
        }
    }


}
