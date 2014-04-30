using System.IO;

namespace Model.Utils
{
    public class BoardStreamWriter
    {
        private readonly Board board;
        private readonly PlayerPieceMapping playerPieceMapping;

        public BoardStreamWriter(Board board)
        {
            this.playerPieceMapping = new PlayerPieceMapping(board);
        }

        public BoardStreamWriter(Board board, PlayerPieceMapping playerPieceMapping)
        {
            this.board = board;
            this.playerPieceMapping = playerPieceMapping;
        }

        private PlayerPieceMapping PlayerPieceMapping
        {
            get { return playerPieceMapping; }
        }

        public void Write(TextWriter textWriter)
        {
            var encoder = new BoardToStringEncoder(board, PlayerPieceMapping);             
            textWriter.Write(encoder.ToString());
        }
    }
}
