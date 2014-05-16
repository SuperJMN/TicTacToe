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
            var board = new TicTacToeBoard();
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
            var board = new TicTacToeBoard();
            var position = new Position(6, 2);

            var player = new HumanPlayer("Pepito");
            var move = new Movement(position, player);
            board.Move(move);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void MoveToSquaresAlreadyTaken()
        {
            var board = new TicTacToeBoard();
            var position = new Position(2, 2);

            var player = new HumanPlayer("Pepito");
            var move = new Movement(position, player);

            board.Move(move);
            board.Move(move);
        }

        [TestMethod]
        public void WinGameHorizontal()
        {
            var board = new TicTacToeBoard();
            var gameOverChecker = new GameOverChecker(board, 3);
            var player = new HumanPlayer("Pepito");
            board.Move(new Movement(new Position(0, 1), player));
            board.Move(new Movement(new Position(1, 1), player));
            board.Move(new Movement(new Position(2, 1), player));

            Assert.IsTrue(gameOverChecker.HasWinner);
        }

        [TestMethod]
        public void WinGameVertical()
        {
            var board = new TicTacToeBoard();
            var gameOverChecker = new GameOverChecker(board, 3);

            var player = new HumanPlayer("Pepito");

            board.Move(new Movement(new Position(0, 0), player));
            board.Move(new Movement(new Position(0, 1), player));
            board.Move(new Movement(new Position(0, 2), player));

            Assert.IsTrue(gameOverChecker.HasWinner);
        }

        [TestMethod]
        public void WinGameDiagonal1()
        {
            var board = new TicTacToeBoard();
            var gameOverChecker = new GameOverChecker(board, 3);
            var player = new HumanPlayer("Pepito");
            board.Move(new Movement(new Position(0, 0), player));
            board.Move(new Movement(new Position(1, 1), player));
            var winningMovement = new Movement(new Position(2, 2), player);
            board.Move(winningMovement);

            Assert.IsTrue(gameOverChecker.IsThisPositionEndingTheGame(winningMovement.Position));
        }

        [TestMethod]
        public void WinGameDiagonal2()
        {
            var board = new TicTacToeBoard();
            var gameOverChecker = new GameOverChecker(board, 3);

            var player = new HumanPlayer("Pepito");
            board.Move(new Movement(new Position(2, 0), player));
            board.Move(new Movement(new Position(1, 1), player));
            var winningMovement = new Movement(new Position(0, 2), player);
            board.Move(winningMovement);

            Assert.IsTrue(gameOverChecker.IsThisPositionEndingTheGame(winningMovement.Position));
        }

        [TestMethod]
        public void FullRowWithBothPlayersShouldNotWin()
        {
            var board = new TicTacToeBoard();
            var player1 = new HumanPlayer("Pepito");
            var player2 = new HumanPlayer("Juanito");

            var gameOverChecker = new GameOverChecker(board, 3);

            board.Move(new Movement(new Position(0, 0), player1));
            board.Move(new Movement(new Position(0, 1), player2));
            board.Move(new Movement(new Position(0, 2), player1));

            Assert.IsFalse(gameOverChecker.HasWinner);
        }

        [TestMethod]
        public void BoardIsIncomplete()
        {
            var board = new TicTacToeBoard();
            Assert.IsFalse(board.IsFull);
        }

        [TestMethod]
        public void BoardIsFull()
        {
            var board = new TicTacToeBoard();
            var player = new HumanPlayer("Pepito");
            FillBoard(player, board);
            Assert.IsTrue(board.IsFull);
        }

        private void FillBoard(Player player, Board board)
        {
            for (var i = 0; i < board.Height; i++)
            {
                for (var j = 0; j < board.Width; j++)
                {
                    board.Move(new Movement(new Position(i, j), player));
                }
            }
        }
    }


}
