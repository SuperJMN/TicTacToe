using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Model.Utils;

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
                    childScore = Math.Min(childScore, GetScore(stateNode));
                }
                return childScore;
            }
        }

        private static int EvaluateTerminalNode(Node node)
        {
            var boardEvaluatorSimple = new BoardEvaluator(node.OriginalBoard);
            var best = boardEvaluatorSimple.Evaluate(node.OriginatingPlayer);

            if (node.CurrentPlayer == node.Max)
            {
                return -best;    
            }
            else
            {
                return best;
            }

            
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

                if (Level + 1 <= MaxLevel)
                {

                    foreach (var emptyPosition in OriginalBoard.GetEmptyPositions().ToList())
                    {
                        var boardSucessor = OriginalBoard.Clone();

                        var movement = new Movement(emptyPosition, CurrentPlayer);
                        boardSucessor.Move(movement);

                        var childNode = new Node(boardSucessor, movement, TwoPlayersGame, Max, Level + 1);
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