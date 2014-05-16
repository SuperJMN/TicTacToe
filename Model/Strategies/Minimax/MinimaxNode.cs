using System;
using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model.Strategies.Minimax
{
    public class MinimaxNode
    {
        private readonly GameOverChecker gameOverChecker;
        private const int MaxDepth = 2;

        public MinimaxNode(Board originalBoard, Movement originatingMovement, ITwoPlayersGame twoPlayersGame, Player max, int depth, GameOverChecker gameOverChecker)
        {
            this.gameOverChecker = gameOverChecker;
            OriginatingMovement = originatingMovement;
            TwoPlayersGame = twoPlayersGame;
            Max = max;
            Depth = depth;
            OriginalBoard = originalBoard;
        }

        private Player OriginatingPlayer
        {
            get { return OriginatingMovement.Player; }
        }

        private int GetScore(MinimaxNode minimaxNode, int alpha, int beta)
        {
            if (minimaxNode.IsTerminal)
            {
                return EvaluateTerminalNode(minimaxNode);
            }

            if (minimaxNode.CurrentPlayer == Max)
            {
                foreach (var stateNode in minimaxNode.Nodes)
                {
                    alpha = Math.Max(alpha, GetScore(stateNode, alpha, beta));
                    if (beta < alpha)
                    {
                        break;
                    }
                }
                return alpha;
            }
            else
            {

                foreach (var stateNode in minimaxNode.Nodes)
                {
                    beta = Math.Min(beta, GetScore(stateNode, alpha, beta));
                    if (beta < alpha)
                    {
                        break;
                    }
                }
                return beta;
            }
        }

        private static int EvaluateTerminalNode(MinimaxNode minimaxNode)
        {
            var boardEvaluatorSimple = new BoardEvaluator(minimaxNode.OriginalBoard);
            var best = boardEvaluatorSimple.Evaluate(minimaxNode.OriginatingPlayer);

            if (minimaxNode.CurrentPlayer == minimaxNode.Max)
            {
                return -best;
            }
            else
            {
                return best;
            }


        }

        public Movement OriginatingMovement { get; private set; }
        private ITwoPlayersGame TwoPlayersGame { get; set; }
        private Player Max { get; set; }

        private bool IsTerminal
        {
            get
            {
                return OriginalBoard.IsFull ||
                       Depth == MaxDepth ||
                       gameOverChecker.HasWinner;
            }
        }

        private int? score;

        public int Score
        {
            get
            {
                if (!score.HasValue)
                {
                    score = GetScore(this, int.MinValue + 1, int.MaxValue - 1);
                }
                return score.Value;
            }
        }

        private int Depth { get; set; }

        public IEnumerable<MinimaxNode> Nodes
        {
            get
            {
                var nodes = new List<MinimaxNode>();

                if (this.IsTerminal)
                {
                    return new List<MinimaxNode>();
                }

                if (Depth + 1 <= MaxDepth)
                {

                    foreach (var emptyPosition in OriginalBoard.EmptyPositions.ToList())
                    {
                        var boardSucessor = OriginalBoard.Clone();

                        var movement = new Movement(emptyPosition, CurrentPlayer);
                        boardSucessor.Move(movement);

                        var childNode = new MinimaxNode(boardSucessor, movement, TwoPlayersGame, Max, Depth + 1, gameOverChecker);
                        nodes.Add(childNode);
                    }
                }

                return nodes;
            }
        }

        private Player CurrentPlayer
        {
            get { return TwoPlayersGame.GetOponent(OriginatingPlayer); }
        }

        private Board OriginalBoard { get; set; }
    }
}