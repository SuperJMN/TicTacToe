using System.Collections.Generic;

namespace Model.Strategies.Minimax
{
    public class BoardState
    {
        public BoardState(Board board, Player relativePlayer)
        {
            Board = board;
            RelativePlayer = relativePlayer;
        }

        public Player RelativePlayer { get; set; }

        public IEnumerable<BoardState> GetSubStates(Player player)
        {
            var children = new List<BoardState>();

            var emptyPositions = Board.GetEmptyPositions();
            foreach (var position in emptyPositions)
            {
                var boardClone = Board.Clone();
                boardClone.Move(new Movement(position, player));
                var child = new BoardState(boardClone, player);
                children.Add(child);
            }

            return children;
        }

        public bool IsTerminal()
        {
            return Board.IsFull || Board.HasWinner;
        }

        public int GetTotalScore()
        {
            var checker = new BoardEvaluator(Board);
            return checker.Evaluate(RelativePlayer);
        }

        private Board Board { get; set; }


    }
}