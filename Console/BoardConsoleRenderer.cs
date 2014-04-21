using Model;

namespace Console
{
    public class BoardConsoleRenderer
    {
        private readonly Board board;
        private readonly PlayerPieceMapping playerPieceMapping;

        public BoardConsoleRenderer(Board board, PlayerPieceMapping playerPieceMapping)
        {
            this.board = board;
            this.playerPieceMapping = playerPieceMapping;
        }

        public void Render()
        {
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


                    System.Console.Write("[" + representation + "]");
                }

                System.Console.WriteLine();
            }
            System.Console.WriteLine();
        }
    }
}
