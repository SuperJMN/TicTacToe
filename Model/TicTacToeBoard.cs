namespace Model
{
    public class TicTacToeBoard : Board
    {
        public TicTacToeBoard() : base(3, 3)
        {
          
        }

        private TicTacToeBoard(TicTacToeBoard ticTacToeBoard) : base(ticTacToeBoard)
        {
            
        }

        public override Board Clone()
        {
            return new TicTacToeBoard(this);
        }
    }
}