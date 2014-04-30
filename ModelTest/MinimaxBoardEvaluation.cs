using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Strategies;
using Model.Strategies.Minimax;

namespace ModelTest
{
    [TestClass]
    public class MinimaxBoardEvaluation
    {
        [TestMethod]
        public void ThreeInARowTest()
        {
            var board = new Board();
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
            var board = new Board();
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
            var board = new Board();
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
            var board = new Board();
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


        //[TestMethod]
        //public void BestMoveBlockage()
        //{
        //    var match = new Match();

        //    var human = new HumanPlayer("Pepito");
        //    var cpu = new ComputerPlayer("Máquina");

        //    cpu.Strategy = new MinimaxStrategy(match, cpu);

        //    match.AddChallenger(human);
        //    match.AddChallenger(cpu);

        //    match.Board.Move(new Movement(new Position(0, 0), human));
        //    match.Board.Move(new Movement(new Position(1, 0), human));
        //    match.Board.Move(new Movement(new Position(0, 1), cpu));

        //    var move = cpu.Strategy.GetMoveFor(match.Board, cpu);
        //    Assert.AreEqual(new Position(2, 0), move.Position);
        //}

        //[TestMethod]
        //public void BestInitialPosition()
        //{
        //    var match = new Match();

        //    var human = new HumanPlayer("Pepito");
        //    var cpu = new ComputerPlayer("Máquina");

        //    cpu.Strategy = new MinimaxStrategy(match, cpu);

        //    match.AddChallenger(human);
        //    match.AddChallenger(cpu);       

        //    var move = cpu.Strategy.GetMoveFor(match.Board, cpu);
        //    Assert.AreEqual(new Position(1, 1), move.Position);
        //}

        //[TestMethod]
        //public void Generation()
        //{
        //    var match = new Match();


        //    var max = new HumanPlayer("Max");
        //    var min = new HumanPlayer("Min");

        //    match.AddChallenger(max);
        //    match.AddChallenger(min);

        //    match.Board.Move(new Movement(new Position(0, 0), max));
        //    match.Board.Move(new Movement(new Position(0, 2), max));
        //    match.Board.Move(new Movement(new Position(2, 2), max));

        //    match.Board.Move(new Movement(new Position(1, 0), min));
        //    match.Board.Move(new Movement(new Position(1, 1), min));
        //    match.Board.Move(new Movement(new Position(0, 1), min));

        //    var node = new Node(match.Board, min, match, max);
        //}

    }


}