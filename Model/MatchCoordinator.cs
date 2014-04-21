using System;

namespace Model
{
    public class MatchCoordinator
    {
        public MatchCoordinator(Match session)
        {
            Session = session;
        }

        public Match Session { get; set; }

        public void PlayerOnWantToMove(Player player, MoveEventHandlerArgs args)
        {
            if (player == Session.PlayerInTurn)
            {
                Session.Board.Move(Session.PlayerInTurn, args.Move);
            }
            else
            {
                throw new InvalidOperationException("The turn should be respected");
            }

            if (CanContinueGame())
            {
                Session.SwitchTurn();
                Session.PlayerInTurn.RequestMove(Session.Board);
            }
            else
            {
                OnGameEnded();
            }            
        }

        private bool CanContinueGame()
        {
            return !Session.Board.IsFull && !Session.Board.BoardChecker.HasWinningRow;
        }

        public event EventHandler GameEnded;

        protected virtual void OnGameEnded()
        {
            var handler = GameEnded;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void StartGame()
        {
            Session.PlayerInTurn.RequestMove(Session.Board);
        }
    }
}