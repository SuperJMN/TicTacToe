namespace Model
{
    public class ComputerPlayer : Player
    {
        private readonly IMoveStrategy strategy;

        public ComputerPlayer(string name, IMoveStrategy strategy)
            : base(name)
        {
            this.strategy = strategy;
        }

        public override void RequestMove(Board board)
        {
            var move = strategy.GetMoveFor(board, this);
            OnWantToMove(new MoveEventHandlerArgs(move));
        }
    }
}