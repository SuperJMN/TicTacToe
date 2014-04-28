namespace Model
{
    public interface IMoveStrategy
    {
        Movement GetMoveFor(Board board, Player player);
    }
}