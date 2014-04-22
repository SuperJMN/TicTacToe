using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Strategies;

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
            Assert.AreEqual(8, value);
        }

        [TestMethod]
        public void EvaluateWithOnePieceInARow()
        {
            var board = new Board();
            var player = new HumanPlayer("Name");
            board.Move(player, new Move(new Position(0, 1)));
            var boardEvaluator = new BoardEvaluator(board);
            var value = boardEvaluator.Evaluate(player);
            Assert.AreEqual(26, value);
        }

        [TestMethod]
        public void EvaluateWithTwoPiecesInARow()
        {
            var board = new Board();
            var player = new HumanPlayer("Name");
            board.Move(player, new Move(new Position(0, 0)));
            board.Move(player, new Move(new Position(0, 1)));
            var boardEvaluator = new BoardEvaluator(board);
            var value = boardEvaluator.Evaluate(player);
            Assert.AreEqual(134, value);
        }

        [TestMethod]
        public void EvaluateWithThreePiecesInARow()
        {
            var board = new Board();
            var player = new HumanPlayer("Name");
            board.Move(player, new Move(new Position(0, 0)));
            board.Move(player, new Move(new Position(0, 1)));
            board.Move(player, new Move(new Position(0, 2)));
            var boardEvaluator = new BoardEvaluator(board);
            var value = boardEvaluator.Evaluate(player);
            Assert.AreEqual(1052, value);
        }

        //[TestMethod]
        //public void BestMove1()
        //{
        //    var board = new Board();
        //    var strategy = new MinimaxStrategy(board);
        //    var player = new ComputerPlayer("Name", strategy);
            

        //    board.Move(player, new Move(new Position(0, 0)));
        //    board.Move(player, new Move(new Position(0, 1)));

        //    var move = strategy.GetMoveFor(board, player);
        //    Assert.AreEqual(new Position(0, 2), move.Position);
        //}

    }


}