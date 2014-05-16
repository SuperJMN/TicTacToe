using System.Text;

namespace Model.Utils
{
    public class BoardToStringEncoder
    {
        public Board Board { get; set; }
        public PlayerPieceMapping Mapping { get; set; }
        



        public BoardToStringEncoder(Board board)
        {
            Mapping = new PlayerPieceMapping(board);
            Board = board;
        }

        public BoardToStringEncoder(Board board, PlayerPieceMapping mapping)
        {
            Board = board;
            Mapping = mapping;
        }


        public override string ToString()
        {
            var builder = new StringBuilder();
            
            for (var y = 0; y < Board.Height; y++)
            {
                for (var j = 0; j < Board.Width; j++)
                {
                    var piece = Board.GetPiece(new Position(j, y));
                    char representation;

                    representation = piece == null ? ' ' : Mapping[piece.Player];

                    builder.Append("[" + representation + "]");
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }


    }
}