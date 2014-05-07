﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.Strategies;
using Model.Utils;

namespace Model
{
    public class Match : ITwoPlayersGame
    {
        private Player playerInTurn;
        public Board Board { get; private set; }
        private bool IsStarted { get; set; }
        public MatchCoordinator Coordinator { get; private set; }

        public Match()
        {
            Contenders = new List<Player>();
            Coordinator = new MatchCoordinator(this);
            Coordinator.GameOver += CoordinatorOnGameOver;

            Board = new Board();
        }

        private void CoordinatorOnGameOver(object sender, EventArgs eventArgs)
        {
            IsFinished = true;
            var gameOverEventArgs = new GameOverEventArgs(Board.WinningLines);

            UnsubscribeFromContenderEvents();
            OnGameOver(gameOverEventArgs);
        }

        public event GameOverEventHandler GameOver;

        protected virtual void OnGameOver(GameOverEventArgs e)
        {
            var handler = GameOver;
            if (handler != null) handler(this, e);
        }

        public event EventHandler Started;

        protected virtual void OnStarted()
        {
            IsStarted = true;

            EventHandler handler = Started;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void AddChallenger(Player player)
        {
            if (Contenders.Count > 1)
            {
                throw new InvalidOperationException("Cannot add more players to the game");
            }
            if (player == null)
            {
                throw new ArgumentException("player");
            }

            Contenders.Add(player);
        }        

        public void SwitchTurn()
        {
            PlayerInTurn = this.GetOponent(PlayerInTurn);
        }        

        public Player PlayerInTurn
        {
            get { return playerInTurn; }
            private set
            {
                playerInTurn = value;
                OnTurnChanged();
            }
        }       
       
        public void Start()
        {
            if (Contenders.Count < 2)
            {
                throw new InvalidOperationException("Cannot start a session without 2 players");
            }
            if (IsStarted)
            {
                throw new InvalidOperationException("Cannot start a game that has already been started");
            }

            SubscribeToContendersEvents();

            PlayerInTurn = Contenders.First();                        
            OnStarted();

            Coordinator.StartGame();
        }

        private void SubscribeToContendersEvents()
        {
            foreach (var contender in Contenders)
            {
                contender.WantToMove += PlayerOnWantToMove;
            }
        }

        private void PlayerOnWantToMove(object sender, PositionEventHandlerArgs args)
        {
            Coordinator.PlayerOnWantToMove(sender, args);
        }

        public Player FirstPlayer { get { return Contenders[0]; } }
        public Player SecondPlayer { get { return Contenders[1]; } }

        public bool HasWinner
        {
            get { return Board.HasWinner; }
        }

        private void UnsubscribeFromContenderEvents()
        {
            foreach (var contender in Contenders)
            {
                contender.WantToMove -= PlayerOnWantToMove;
            }
        }

        public IEnumerable<WinningLine> WinningLines
        {
            get { return Board.WinningLines; }
        }

        public bool IsFinished { get; private set; }

        public event EventHandler TurnChanged;

        protected virtual void OnTurnChanged()
        {
            var handler = TurnChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public IList<Player> Contenders { get; private set; }
    }
}