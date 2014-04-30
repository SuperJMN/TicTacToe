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

        public Player OriginatingPlayer
        {
            get { return OriginatingMovement.Player; }
        }

        private int GetScore(Node node, Player player)
        {
            if (node.IsTerminal || Level < MaxLevel)
            {
                var boardEvaluatorSimple = new BoardEvaluator(node.OriginalBoard);
                var best = boardEvaluatorSimple.Evaluate(player);                

                return best;
            }

            if (player == Max)
            {
                var score = int.MinValue + 1;
                foreach (var stateNode in node.Nodes)
                {
                    score = Math.Max(score, GetScore(stateNode, GetOponent(player)));
                }
                return score;
            }
            else
            {
                var score = int.MaxValue - 1;
                foreach (var stateNode in node.Nodes)
                {
                    score = Math.Min(score, GetScore(stateNode, GetOponent(player)));
                }
                return score;
            }
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
            get { return OriginalBoard.IsFull || OriginalBoard.HasWinner; }
        }

        private int? score;

        public int Score
        {
            get
            {
                if (!score.HasValue)
                {
                    score = GetScore(this, OriginatingMovement.Player);
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

                foreach (var emptyPosition in OriginalBoard.GetEmptyPositions().ToList())
                {
                    var boardSucessor = OriginalBoard.Clone();

                    var movement = new Movement(emptyPosition, Oponent);
                    boardSucessor.Move(movement);

                    var childNode = new Node(boardSucessor, new Movement(emptyPosition, Oponent), TwoPlayersGame, Max, Level + 1);
                    nodes.Add(childNode);
                }

                return nodes;
            }
        }

        public Player Oponent
        {
            get
            {
                return OriginatingPlayer == TwoPlayersGame.FirstPlayer
                    ? this.TwoPlayersGame.SecondPlayer
                    : this.TwoPlayersGame.FirstPlayer;
            }
        }

        private Board OriginalBoard { get; set; }
    }
}