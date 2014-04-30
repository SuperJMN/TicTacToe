using System.Diagnostics;

namespace Model
{
    public class BoardDebugView
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Board Board { get; set; }

        public BoardDebugView(Board board)
        {
            Board = board;
        }

        public SquareCollection Row1
        {
            get { return Board.Rows[0]; }
        }

        public SquareCollection Row2 { get { return Board.Rows[1]; } }
        public SquareCollection Row3 { get { return Board.Rows[2]; } }
    }
}