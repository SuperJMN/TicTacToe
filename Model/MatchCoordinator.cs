using System;

namespace Model
{
    public class MatchCoordinator
    {
        public MatchCoordinator(Match match)
        {
            Match = match;
        }

        private Match Match { get; set; }

        public void PlayerOnWantToMove(object sender, PositionEventHandlerArgs e)
        {
            var player = (Player)sender;
            var movement = new Movement(e.Position, player);

            if (player == Match.PlayerInTurn && !Match.IsFinished)
            {
                Match.Board.Move(movement);
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

        public event EventHandler GameOver;

        private void OnGameEnded()
        {
            var handler = GameOver;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void StartGame()
        {
            Match.PlayerInTurn.RequestMove(Match.Board);
        }
    }
}