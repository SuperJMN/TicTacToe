using Model.Strategies;

namespace Model
{
    public class ComputerPlayer : Player
    {
        
        public ComputerPlayer(string name)
            : base(name)
        {
            Strategy = new DefaultComputerStrategy();
        }

        public IMoveStrategy Strategy { get; set; }

        public override void RequestMove(Board board)
        {
            var move = Strategy.GetMoveFor(board, this);
            OnWantToMove(new PositionEventHandlerArgs(move.Position));
        }
    }
}