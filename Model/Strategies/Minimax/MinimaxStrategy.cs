using System.Linq;
using Model.Utils;

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
            var root = new Node(board, RootGeneratingMovement, TwoPlayersGame, Max, 0);
            var bestNode = root.Nodes.First(node => node.Score == root.Score);
            return bestNode.OriginatingMovement;
        }

        private Movement RootGeneratingMovement
        {
            get { return new Movement(new Position(-1, -1), TwoPlayersGame.GetOponent(Max)); }
        }    
    }
}