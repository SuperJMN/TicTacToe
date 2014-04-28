namespace Model.Strategies
{
    public interface ITwoPlayersGame
    {
        Player FirstPlayer { get; }
        Player SecondPlayer { get; }
    }
}