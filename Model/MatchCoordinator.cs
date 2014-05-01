using System;
using System.Diagnostics;

namespace Model
{
    public class MatchCoordinator
    {
        public MatchCoordinator(Match match)
        {
            Match = match;
        }

        public Match Match { get; set; }

        public void PlayerOnWantToMove(Player player, MoveEventHandlerArgs args)
        {
            if (player == Match.PlayerInTurn)
            {
                Match.Board.Move(args.Movement);
            }
            else
            {
                throw new InvalidOperationException("The turn should be respected");
            }

            if (CanContinueGame())
            {
                Match.SwitchTurn();
                Match.PlayerInTurn.RequestMove(Match.Board);
            }
            else
            {
                OnGameEnded();
            }            
        }

        private bool CanContinueGame()
        {
            return !Match.Board.IsFull && !Match.HasWinner;
        }

        public event EventHandler GameEnded;

        protected virtual void OnGameEnded()
        {
            var handler = GameEnded;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void StartGame()
        {
            Match.PlayerInTurn.RequestMove(Match.Board);
        }
    }
}