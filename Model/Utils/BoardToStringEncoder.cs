using System.Collections;
using System.Text;

namespace Model.Utils
{
    public class BoardToStringEncoder
    {
        public Board Board { get; set; }
        public PlayerPieceMapping Mapping { get; set; }
        private int Tabs { get; set; }



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
            
            for (var i = 0; i < Board.BoardSize; i++)
            {
                for (var k = 0; k < Tabs; k++)
                {
                    builder.Append("\t");
                }

                for (var j = 0; j < Board.BoardSize; j++)
                {
                    var piece = Board.GetPiece(new Position(j, i));
                    char representation;

                    if (piece == null)
                    {
                        representation = ' ';
                    }
                    else
                    {
                        representation = Mapping[piece.Player];
                    }

                    builder.Append("[" + representation + "]");
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }


    }
}