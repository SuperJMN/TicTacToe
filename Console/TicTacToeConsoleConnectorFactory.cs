using Model;

namespace Console
{
    class TicTacToeConsoleConnectorFactory : HumanPlayerConsoleConnectorFactory
    {
        public override HumanPlayerConsoleConnector CreateConnector(HumanPlayer player, char piece, Board board)
        {
            return new TicTacToeHumanPlayerConsoleConnector(player, piece);
        }
    }
}