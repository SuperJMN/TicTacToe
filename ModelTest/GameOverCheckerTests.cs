using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ModelTest
{
    [TestClass]
    public class GameOverCheckerTests
    {
        [TestMethod]
        public void SquareCollectionWithNoGameOverTest()
        {
            var player1 = new HumanPlayer("Pepito");
            var board = new ConnectFourBoard();
            board.Move(new Movement(new Position(0, 5), player1));
            board.Move(new Movement(new Position(2, 5), player1 ));
            board.Move(new Movement(new Position(3, 5), player1));
            board.Move(new Movement(new Position(4, 5), player1));
            board.Move(new Movement(new Position(6, 5), player1));

            var cameOverChecker = new GameOverChecker(board, 4);
            var isGameOver = cameOverChecker.IsThisPositionEndingTheGame(new Position(3,5));

            Assert.IsFalse(isGameOver);
        }

        [TestMethod]
        public void SquareCollectionWithGameOverTest()
        {
            var player1 = new HumanPlayer("Pepito");
            var board = new ConnectFourBoard();
            board.Move(new Movement(new Position(0, 5), player1));
            board.Move(new Movement(new Position(2, 5), player1));
            board.Move(new Movement(new Position(3, 5), player1));
            board.Move(new Movement(new Position(4, 5), player1));
            board.Move(new Movement(new Position(5, 5), player1));

            var cameOverChecker = new GameOverChecker(board, 4);
            var isGameOver = cameOverChecker.IsThisPositionEndingTheGame(new Position(3, 5));

            Assert.IsTrue(isGameOver);
        }
    }
}