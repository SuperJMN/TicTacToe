using System.Collections.Generic;

namespace Model
{
    public class ConnectFourBoard : Board
    {
        public ConnectFourBoard()
            : base(7, 6)
        {
            
        }

        private ConnectFourBoard(ConnectFourBoard ticTacToeBoard)
            : base(ticTacToeBoard)
        {

        }

        public override IEnumerable<Position> GetValidMovePositions()
        {
            var positions = new List<Position>();
            foreach (var column in Columns)
            {
                positions.AddRange(GetValidMoveFrom(column));
            }
            return positions;
        }

        private IEnumerable<Position> GetValidMoveFrom(SquareList column)
        {
            var positions = new List<Position>();
            for (int i = Height - 1; i >= 0; i--)
            {
                if (column[i].Piece == null)
                {
                    positions.Add(column[i].Position);
                    break;
                }
            }
            return positions;
        }

        public override Board Clone()
        {
            return new ConnectFourBoard(this);
        }
    }
}