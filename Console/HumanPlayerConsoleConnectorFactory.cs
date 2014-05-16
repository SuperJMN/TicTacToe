using Model;

namespace Console
{
    internal abstract class HumanPlayerConsoleConnectorFactory
    {
        public abstract HumanPlayerConsoleConnector CreateConnector(HumanPlayer player, char piece, Board board);
    }
}