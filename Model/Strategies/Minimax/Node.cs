using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Model.Strategies.Minimax
{
    public class Node
    {
        private const int MaxLevel = 8;

        public Node(Board originalBoard, Movement originatingMovement, ITwoPlayersGame twoPlayersGame, Player max, int level)
        {
            OriginatingMovement = originatingMovement;
            TwoPlayersGame = twoPlayersGame;
            Max = max;
            Level = level;
            OriginalBoard = originalBoard;
        }

        private Player OriginatingPlayer
        {
            get { return OriginatingMovement.Player; }
        }

        private int GetScore(Node node)
        {
            if (node.IsTerminal)
            {
                return EvaluateTerminalNode(node);
            }

            if (node.CurrentPlayer == Max)
            {
                var childScore = int.MinValue + 1;
                foreach (var stateNode in node.Nodes)
                {
                    childScore = Math.Max(childScore, GetScore(stateNode));
                }
                return childScore;
            }
            else
            {
                var childScore = int.MaxValue - 1;
                foreach (var stateNode in node.Nodes)
                {
                    var currentScore = -GetScore(stateNode);
                    childScore = Math.Min(childScore, currentScore);
                }
                return childScore;
            }
        }

        private static int EvaluateTerminalNode(Node node)
        {
            var boardEvaluatorSimple = new BoardEvaluator(node.OriginalBoard);
            var best = boardEvaluatorSimple.Evaluate(node.OriginatingPlayer);
            return best;
        }

        private Player GetOponent(Player player)
        {
            return player == TwoPlayersGame.FirstPlayer ? TwoPlayersGame.SecondPlayer : TwoPlayersGame.FirstPlayer;
        }

        public Movement OriginatingMovement { get; set; }
        private ITwoPlayersGame TwoPlayersGame { get; set; }
        private Player Max { get; set; }

        private bool IsTerminal
        {
            get
            {
                return OriginalBoard.IsFull ||
                    Level == MaxLevel ||
                    OriginalBoard.HasWinner;
            }
        }

        private int? score;

        public int Score
        {
            get
            {
                if (!score.HasValue)
                {
                    score = GetScore(this);
                }
                return score.Value;
            }
        }

        public int Level { get; set; }

        public IEnumerable<Node> Nodes
        {
            get
            {
                var nodes = new List<Node>();

                if (this.IsTerminal)
                {
                    return new List<Node>();
                }

                var nextLevel = Level + 1;

                if (nextLevel <= MaxLevel)
                {

                    foreach (var emptyPosition in OriginalBoard.GetEmptyPositions().ToList())
                    {
                        var boardSucessor = OriginalBoard.Clone();

                        var nextPlayer = CurrentPlayer;
                        var movement = new Movement(emptyPosition, nextPlayer);
                        boardSucessor.Move(movement);

                        var childNode = new Node(boardSucessor, movement, TwoPlayersGame, Max, nextLevel);
                        nodes.Add(childNode);
                    }
                }

                return nodes;
            }
        }

        private Player CurrentPlayer
        {
            get { return GetOponent(OriginatingPlayer); }
        }

        private Board OriginalBoard { get; set; }
    }
}