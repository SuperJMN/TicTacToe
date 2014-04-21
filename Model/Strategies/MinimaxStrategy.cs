namespace Model.Strategies
{
    public class MinimaxStrategy : IMoveStrategy
    {
        public Move GetMoveFor(Board board)
        {
            return new Move(new Position(0, 0));
        }
    }
}