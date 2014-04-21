﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Strategies;

namespace ModelTest
{
    [TestClass]
    public class MatchTests
    {

        [TestMethod]
        public void AddOnePlayer()
        {
            var session = new Match();
            session.AddChallenger(new HumanPlayer("Pepito"));
            Assert.AreEqual(1, session.Contenders.Count);
        }

        [TestMethod]
        public void AddTwoPlayers()
        {
            var session = new Match();
            session.AddChallenger(new HumanPlayer("Pepito"));
            session.AddChallenger(new HumanPlayer("Juanito"));
            Assert.AreEqual(2, session.Contenders.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddThreePlayers()
        {
            var session = new Match();
            session.AddChallenger(new HumanPlayer("Pepito"));
            session.AddChallenger(new HumanPlayer("Juanito"));
            session.AddChallenger(new HumanPlayer("Jorgito"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StartInvalidSession()
        {
            var session = new Match();
            session.AddChallenger(new HumanPlayer("Pepito"));
            session.Start();
        }

        [TestMethod]
        public void StartSessionVerifyFirstTurn()
        {
            var session = new Match();
            var humanPlayer = new HumanPlayer("Pepito");
            session.AddChallenger(humanPlayer);
            session.AddChallenger(new HumanPlayer("Juanito"));
            session.Start();
            Assert.AreEqual(humanPlayer, session.PlayerInTurn);
        }

        [TestMethod]
        public void StartSessionVerifyNextTurn()
        {
            var session = new Match();

            var firstPlayer = new HumanPlayer("Pepito");
            session.AddChallenger(firstPlayer);

            var secondPlayer = new HumanPlayer("Juanito");
            session.AddChallenger(secondPlayer);

            session.Start();

            firstPlayer.MakeMove(new Move(new Position(0,0)));

            Assert.AreEqual(secondPlayer, session.PlayerInTurn);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StartSessionPlayerWantsToRepeatTurn()
        {
            var session = new Match();

            var firstPlayer = new HumanPlayer("Pepito");
            session.AddChallenger(firstPlayer);

            var secondPlayer = new HumanPlayer("Juanito");
            session.AddChallenger(secondPlayer);

            session.Start();

            firstPlayer.MakeMove(new Move(new Position(0, 0)));
            firstPlayer.MakeMove(new Move(new Position(1, 0)));            
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void PlayerMovesToTakenSlot()
        {
            var session = new Match();

            var firstPlayer = new HumanPlayer("Pepito");
            session.AddChallenger(firstPlayer);

            var secondPlayer = new HumanPlayer("Juanito");
            session.AddChallenger(secondPlayer);

            session.Start();

            firstPlayer.MakeMove(new Move(new Position(0, 0)));
            secondPlayer.MakeMove(new Move(new Position(0, 0)));
        }


        [TestMethod]        
        public void ComputerPlay()
        {
            var session = new Match();

            var strategy = new DefaultComputerStrategy();

            var firstPlayer = new ComputerPlayer("Pepito", strategy);
            session.AddChallenger(firstPlayer);

            var secondPlayer = new ComputerPlayer("Juanito", strategy);
            session.AddChallenger(secondPlayer);

            session.Coordinator.GameEnded += (sender, args) => Assert.IsTrue(session.Finished);
            session.Start();                       
        }

        private void CoordinatorOnGameEnded(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }

    
}