using System.Collections;
using System.Linq;

namespace Model.Strategies.Minimax
{
    public class MinimaxStrategy : IMoveStrategy
    {
        public MinimaxStrategy(ITwoPlayersGame twoPlayersGame, Player max)
        {
            TwoPlayersGame = twoPlayersGame;
            Max = max;
        }

        private ITwoPlayersGame TwoPlayersGame { get; set; }
        private Player Max { get; set; }

        public Movement GetMoveFor(Board board, Player player)
        {
            var root = new Node(board, new Movement(new Position(-1, -1), GetOponent(Max)), TwoPlayersGame, Max, 0);


            var max = root.Score;

            var result = root.Nodes.First(node => node.Score == max);

            return result.OriginatingMovement;
        }


        private Player GetOponent(Player player)
        {
            return player == TwoPlayersGame.FirstPlayer ? TwoPlayersGame.SecondPlayer : TwoPlayersGame.FirstPlayer;
        }
    }
}