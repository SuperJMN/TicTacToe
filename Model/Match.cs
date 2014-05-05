using System;
using System.Collections.Generic;
using System.Linq;
using Model.Strategies;

namespace Model
{
    public class Match : ITwoPlayersGame, IDisposable
    {
        private Player playerInTurn;
        public Board Board { get; private set; }
        private bool IsStarted { get; set; }

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
            EventHandler handler = Started;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void AddChallenger(Player player)
        {
            if (Contenders.Count > 1)
            {
                throw new InvalidOperationException("Cannot add more players to the game");
            }
            if (player==null)
            {
                throw new ArgumentException("player");
            }

            player.WantToMove += PlayerOnWantToMove;
            Contenders.Add(player);
        }

        private void PlayerOnWantToMove(object sender, PositionEventHandlerArgs args)
        {
            Coordinator.PlayerOnWantToMove(sender, args);
        }

        public MatchCoordinator Coordinator { get; private set; }

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

        public Player PlayerInTurn
        {
            get { return playerInTurn; }
            set
            {
                playerInTurn = value;
                OnTurnChanged();
            }
        }

        public event EventHandler TurnChanged;

        protected virtual void OnTurnChanged()
        {
            var handler = TurnChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public bool IsFinished { get; private set; }

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

        public void Dispose()
        {
            foreach (var contender in Contenders)
            {
                contender.WantToMove -= PlayerOnWantToMove;
            }
        }
    }
}