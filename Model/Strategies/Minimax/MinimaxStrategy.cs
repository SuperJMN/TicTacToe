using System.Linq;
using Model.Utils;

namespace Model.Strategies.Minimax
{
    public class MinimaxStrategy : IMoveStrategy
    {
        private readonly GameOverChecker gameOverChecker;
        private readonly IBoardEvaluator boardEvaluator;

        public MinimaxStrategy(ITwoPlayersGame twoPlayersGame, Player max, GameOverChecker gameOverChecker, IBoardEvaluator boardEvaluator)
        {
            this.gameOverChecker = gameOverChecker;
            this.boardEvaluator = boardEvaluator;
            TwoPlayersGame = twoPlayersGame;
            Max = max;
        }

        private ITwoPlayersGame TwoPlayersGame { get; set; }
        private Player Max { get; set; }

        public Movement GetMoveFor(Board board, Player player)
        {
            var root = new MinimaxNode(board, RootGeneratingMovement, TwoPlayersGame, Max, 0, gameOverChecker, boardEvaluator);
            var bestNode = root.Nodes.First(node => node.Score == root.Score);
            return bestNode.OriginatingMovement;
        }

        private Movement RootGeneratingMovement
        {
            get { return new Movement(new Position(-1, -1), TwoPlayersGame.GetOponent(Max)); }
        }    
    }
}