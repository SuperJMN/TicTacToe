using Model.Strategies;

namespace Model.Utils
{
    public static class Extensions
    {
        public static Player GetOponent(this ITwoPlayersGame game, Player player)
        {
            return player == game.FirstPlayer ? game.SecondPlayer : game.FirstPlayer;
        }        
    }
}