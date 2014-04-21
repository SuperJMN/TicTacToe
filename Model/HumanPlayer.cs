using System;

namespace Model
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name)
            : base(name)
        {
        }

        public override void RequestMove(Board board)
        {
            OnMoveRequested();
        }

        public event EventHandler MoveRequested;

        protected virtual void OnMoveRequested()
        {
            var handler = MoveRequested;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}