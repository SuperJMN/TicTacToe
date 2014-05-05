using System;
using Model.Strategies;
using Model.Strategies.Minimax;

namespace Model
{
    public class PlayerFactory
    {
        private ITwoPlayersGame TwoPlayersGame { get; set; }

        public PlayerFactory(ITwoPlayersGame twoPlayersGame)
        {
            TwoPlayersGame = twoPlayersGame;
        }

        public Player CreatePlayer(string name, PlayerType type)
        {
            switch (type)
            {
                case PlayerType.Human:
                    return CreateHumanPlayer(name);
                case PlayerType.ComputerMinimax:
                    return CreateComputerPlayerMinimax(name);
                case PlayerType.ComputerRandom:
                    return CreateComputerPlayerRandom(name);
                case PlayerType.ComputerDefault:
                    return CreateComputerPlayerDefault(name);
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        private static Player CreateComputerPlayerDefault(string name)
        {
            return new ComputerPlayer(name);
        }

        private ComputerPlayer CreateComputerPlayerMinimax(string name)
        {
            var computer = new ComputerPlayer(name);
            computer.Strategy = new MinimaxStrategy(TwoPlayersGame, computer);

            return computer;
        }

        private ComputerPlayer CreateComputerPlayerRandom(string name)
        {
            var computer = new ComputerPlayer(name) {Strategy = new RandomStrategy()};

            return computer;
        }

        private HumanPlayer CreateHumanPlayer(string name)
        {
            var human = new HumanPlayer(name);
            return human;
        }
    }
}