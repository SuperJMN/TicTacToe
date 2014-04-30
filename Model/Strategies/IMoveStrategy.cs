namespace Model.Strategies
{
    public interface IMoveStrategy
    {
        Movement GetMoveFor(Board board, Player player);
    }
}