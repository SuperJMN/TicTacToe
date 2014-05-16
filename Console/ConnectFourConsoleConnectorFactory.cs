using Model;

namespace Console
{
    class ConnectFourConsoleConnectorFactory : HumanPlayerConsoleConnectorFactory
    {
        public override HumanPlayerConsoleConnector CreateConnector(HumanPlayer player, char piece, Board board)
        {
            return new ConnectFourHumanPlayerConsoleConnector(player, piece, board);
        }
    }
}