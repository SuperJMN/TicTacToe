using System;
using System.Collections.Generic;
using System.Linq;
using Model.Strategies;

namespace Model
{
    public class Match : ITwoPlayersGame
    {
        public Board Board { get; private set; }
        public bool IsStarted { get; private set; }

        public Match()
        {
            Contenders = new List<Player>();
            Coordinator = new MatchCoordinator(this);
            Coordinator.GameEnded += CoordinatorOnGameEnded;

            Board = new Board();
        }

        private void CoordinatorOnGameEnded(object sender, EventArgs eventArgs)
        {
            IsFinished = true;
            OnFinished();
        }

        public event EventHandler Finished;
        public event EventHandler Started;

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnFinished()
        {
            EventHandler handler = Finished;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void AddChallenger(Player player)
        {
            if (Contenders.Count > 1)
            {
                throw new InvalidOperationException("Cannot add more players to the game");
            }

            player.WantToMove += (p, args) => Coordinator.PlayerOnWantToMove((Player)p, args);
            Contenders.Add(player);
        }

        public MatchCoordinator Coordinator { get; set; }

        public void SwitchTurn()
        {
            if (PlayerInTurn == Contenders[0])
            {
                PlayerInTurn = Contenders[1];
            }
            else
            {
                PlayerInTurn = Contenders[0];
            }
        }

        public IList<Player> Contenders { get; private set; }

        public Player PlayerInTurn { get; set; }
        public bool IsFinished { get; set; }

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

            IsStarted = true;
            OnStarted();
            PlayerInTurn = Contenders.First();
            Coordinator.StartGame();
        }

        public Player FirstPlayer { get { return Contenders[0]; } }
        public Player SecondPlayer { get { return Contenders[1]; } }

        public bool HasWinner
        {
            get { return Board.HasWinner; }
        }

        public Player GetWinner()
        {
            var winner = Board.GetPlayersWithLine().FirstOrDefault();
            return winner;
        }
    }
}