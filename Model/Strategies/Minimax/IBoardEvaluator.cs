namespace Model.Strategies.Minimax
{
    public interface IBoardEvaluator
    {
        int Evaluate(Player player);
    }
}