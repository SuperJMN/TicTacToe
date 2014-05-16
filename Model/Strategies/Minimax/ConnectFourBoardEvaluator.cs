namespace Model.Strategies.Minimax
{
    public class ConnectFourBoardEvaluator : IBoardEvaluator
    {
        private readonly ConnectFourBoard board;

        public ConnectFourBoardEvaluator(ConnectFourBoard board)
        {
            this.board = board;            
        }

        public int Evaluate(Player player)
        {
            return 0;
        }
    }
}