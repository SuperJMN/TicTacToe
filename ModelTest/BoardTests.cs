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

            var player = new HumanPlayer("Pepito");
            var move = new Movement(position, player);
            board.Move(move);

            var piece = board.GetPiece(position);
            Assert.AreEqual(player, piece.Player);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void MoveOutOfBoard()
        {
            var board = new Board();
            var position = new Position(6, 2);

            var player = new HumanPlayer("Pepito");
            var move = new Movement(position, player);
            board.Move(move);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void MoveToSlotAlreadyTaken()
        {
            var board = new Board();
            var position = new Position(2, 2);

            var player = new HumanPlayer("Pepito");
            var move = new Movement(position, player);

            board.Move(move);
            board.Move(move);
        }

        [TestMethod]
        public void WinGameHorizontal()
        {
            var board = new Board();
            var player = new HumanPlayer("Pepito");
            board.Move(new Movement(new Position(0, 1), player));
            board.Move(new Movement(new Position(1, 1), player));
            board.Move(new Movement(new Position(2, 1), player));

            Assert.IsTrue(board.HasWinner);
        }

        [TestMethod]
        public void WinGameVertical()
        {
            var board = new Board();
            var player = new HumanPlayer("Pepito");
            board.Move(new Movement(new Position(0, 0), player));
            board.Move(new Movement(new Position(0, 1), player));
            board.Move(new Movement(new Position(0, 2), player));

            Assert.IsTrue(board.HasWinner);
        }


        [TestMethod]
        public void FullRowWithBothPlayersShouldNotWin()
        {
            var board = new Board();
            var player1 = new HumanPlayer("Pepito");
            var player2 = new HumanPlayer("Juanito");
            board.Move(new Movement(new Position(0, 0), player1));
            board.Move(new Movement(new Position(0, 1), player2));
            board.Move(new Movement(new Position(0, 2), player1));

            Assert.IsFalse(board.HasWinner);
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
                    board.Move(new Movement(new Position(i, j), player));
                }
            }
        }
    }


}
