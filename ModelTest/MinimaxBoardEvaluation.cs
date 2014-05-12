using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Strategies.Minimax;

namespace ModelTest
{
    [TestClass]
    public class MinimaxBoardEvaluation
    {
        [TestMethod]
        public void ThreeInARowTest()
        {
            var board = new TicTacToeBoard();
            var player1 = new HumanPlayer("First");
            
            board.Move(new Movement(new Position(0, 0), player1));
            board.Move(new Movement(new Position(0, 1), player1));
            board.Move(new Movement(new Position(0, 2), player1));

            var boardEvaluator = new BoardEvaluator(board);
            var score = boardEvaluator.Evaluate(player1);
            Assert.AreEqual(105, score);
        }

        [TestMethod]
        public void ThreeInARowTest2()
        {
            var board = new TicTacToeBoard();
            var player1 = new HumanPlayer("First");

            board.Move(new Movement(new Position(0, 0), player1));
            board.Move(new Movement(new Position(1, 1), player1));
            board.Move(new Movement(new Position(2, 2), player1));

            var boardEvaluator = new BoardEvaluator(board);
            var score = boardEvaluator.Evaluate(player1);
            Assert.AreEqual(107, score);
        }

        [TestMethod]
        public void ThreeInARowTest3()
        {
            var board = new TicTacToeBoard();
            var player1 = new HumanPlayer("First");
            var player2 = new HumanPlayer("Second");

            board.Move(new Movement(new Position(0, 0), player1));
            board.Move(new Movement(new Position(1, 1), player2));
            board.Move(new Movement(new Position(2, 2), player1));

            var boardEvaluator = new BoardEvaluator(board);
            var score = boardEvaluator.Evaluate(player1);
            Assert.AreEqual(1, score);
        }

        [TestMethod]
        public void SwitchPlayerMustReturnNegativeScoreTest1()
        {
            var board = new TicTacToeBoard();
            var player1 = new HumanPlayer("First");
            var player2 = new HumanPlayer("Second");

            board.Move(new Movement(new Position(0, 0), player1));
            board.Move(new Movement(new Position(1, 1), player2));
            board.Move(new Movement(new Position(2, 2), player1));

            var boardEvaluator = new BoardEvaluator(board);
            var score1 = boardEvaluator.Evaluate(player1);
            var score2 = boardEvaluator.Evaluate(player2);
            Assert.AreEqual(-score1, score2);
        }
    }


}