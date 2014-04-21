using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Match
    {
        public Board Board { get; set; }

        public Match()
        {
            Contenders = new List<Player>();
            Coordinator = new MatchCoordinator(this);
            Coordinator.GameEnded += CoordinatorOnGameEnded;
            
            Board = new Board();
        }

        private void CoordinatorOnGameEnded(object sender, EventArgs eventArgs)
        {
            Finished = true;
        }

        public void AddChallenger(Player player)
        {
            if (Contenders.Count > 1)
            {
                throw new InvalidOperationException("Cannot add more players to the game");
            }

            player.WantToMove += (p, args) => Coordinator.PlayerOnWantToMove((Player) p, args);
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
        public bool Finished { get; set; }

        public void Start()
        {
            if (Contenders.Count < 2)
            {
                throw new InvalidOperationException("Cannot start a session without 2 players");
            }

            PlayerInTurn = Contenders.First();
            Coordinator.StartGame();
        }
    }
}