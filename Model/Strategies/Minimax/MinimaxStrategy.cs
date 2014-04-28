using System;

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

        public Move GetMoveFor(Board board, Player player)
        {
            var maxScore = int.MinValue + 1;
            var finalPosition = new Position(-1, -1);

            foreach (var position in board.GetEmptyPositions())
            {
                var clone = board.Clone();
                clone.Move(player, new Move(position));
                var node = new BoardState(clone, player);

                var score = Iterate(node, player);
                if (score > maxScore)
                {
                    finalPosition = position;
                    maxScore = score;
                }
            }

            return new Move(finalPosition);
        }

        private int Iterate(BoardState boardState, Player player)
        {
            if (boardState.IsTerminal())
            {
                return boardState.GetTotalScore();
            }

            if (player == Max)
            {
                var score = int.MinValue + 1;
                foreach (BoardState child in boardState.GetSubStates(player))
                {
                    score = Math.Max(Iterate(child, GetOponent(player)), score);
                }

                return score;
            }
            else
            {
                var score = int.MaxValue - 1;
                foreach (BoardState child in boardState.GetSubStates(player))
                {
                    score = Math.Min(Iterate(child, GetOponent(player)), score);
                }

                return -score;
            }
        }

        private Player GetOponent(Player player)
        {
            return player.Equals(TwoPlayersGame.FirstPlayer) ? TwoPlayersGame.SecondPlayer : TwoPlayersGame.FirstPlayer;
        }
    }
}