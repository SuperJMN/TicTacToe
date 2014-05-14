using System.IO;
using Model;

namespace Console
{
    class TicTacToeHumanPlayerConsoleConnector : HumanPlayerConsoleConnector
    {
        public TicTacToeHumanPlayerConsoleConnector(HumanPlayer player, char pieceChar) : base(player, pieceChar)
        {
        }

        protected override Position GetPosition(TextReader input)
        {
            var x = PromptForInteger(input, "Column");
            var y = PromptForInteger(input, "Row");

            return new Position(x, y);
        }
    }
}