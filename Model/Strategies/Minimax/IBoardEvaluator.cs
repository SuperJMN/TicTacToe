namespace Model.Strategies.Minimax
{
    public interface IBoardEvaluator
    {
        int Evaluate(Board board, Player player);
    }
}