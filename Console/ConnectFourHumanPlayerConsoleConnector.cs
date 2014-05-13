using System.IO;
using System.Linq;
using Model;

namespace Console
{
    class ConnectFourHumanPlayerConsoleConnector : HumanPlayerConsoleConnector
    {
        private Board Board { get; set; }

        public ConnectFourHumanPlayerConsoleConnector(HumanPlayer player, char pieceChar, Board board) : base(player, pieceChar)
        {
            Board = board;
        }

        protected override Position GetPosition(TextReader input)
        {
            var x = PromptForInteger(input, "Column");

            var finalPosition = Board.GetValidMovePositions().FirstOrDefault(position => position.X == x);

            if (finalPosition == null)
            {
                throw new InvalidPositionException();
            }

            return finalPosition;
        }
    }
}