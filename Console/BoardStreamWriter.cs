using System.IO;
using System.Text;
using Model;
using Model.Strategies;

namespace Console
{
    public class BoardStreamWriter
    {
        private readonly Board board;
        private readonly PlayerPieceMapping playerPieceMapping;

        public BoardStreamWriter(Board board, PlayerPieceMapping playerPieceMapping)
        {
            this.board = board;
            this.playerPieceMapping = playerPieceMapping;
        }

        public void Write(TextWriter textWriter)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < Board.BoardSize; i++)
            {
                for (var j = 0; j < Board.BoardSize; j++)
                {
                    var piece = board.GetPiece(new Position(j, i));
                    char representation;

                    if (piece == null)
                    {
                        representation = ' ';
                    }
                    else
                    {
                        representation = playerPieceMapping[piece.Player];
                    }


                    builder.Append("[" + representation + "]");
                }

                builder.AppendLine();
            }
            builder.AppendLine();

            textWriter.Write(builder.ToString());
        }
    }
}
