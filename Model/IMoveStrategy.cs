namespace Model
{
    public interface IMoveStrategy
    {
        Move GetMoveFor(Board board, Player player);
    }
}