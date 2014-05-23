using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Utils;

namespace Console
{
    static class Program
    {
        static readonly Dictionary<Player, int> Scores = new Dictionary<Player, int>();

        public static void Main()
        {
            var matchFactory = new ConnectFourMatchFactory();
            var connectorFactory = new ConnectFourConsoleConnectorFactory();

            var i = 1;
            for (var t = 0; t < i; t++)
            {

                var matchConfiguration = new MatchConfiguration
                {
                    Player1 = new PlayerInfo("JMN", PlayerType.Human),
                    Player2 = new PlayerInfo("Anytta", PlayerType.ComputerMinimax),
                };


                var match = matchFactory.CreateMatch(matchConfiguration);
                
                var createMapping = CreateMapping(match);

                var decoratedMatch = new ConsoleMatchDecorator(match, connectorFactory, createMapping);
                decoratedMatch.Start();

                RegisterScores(decoratedMatch.WinningLines);
            }

            ShowScores(i);
            System.Console.ReadLine();
        }

        private static PlayerPieceMapping CreateMapping(Match match)
        {
            var playerPieceMapphing = new PlayerPieceMapping();
            playerPieceMapphing.Add(match.FirstPlayer, 'X');
            playerPieceMapphing.Add(match.SecondPlayer, 'O');
            return playerPieceMapphing;
        }

        private static void ShowScores(int playedGames)
        {
            System.Console.WriteLine("Total matches played: {0}", playedGames);

            var wins = 0;
            foreach (var score in Scores)
            {
                wins += score.Value;
                System.Console.WriteLine("{0} won {1} times", score.Key, score.Value);
            }

            var draws = playedGames-wins;
            System.Console.WriteLine("There have been {0} draws", draws);
        }

        private static void RegisterScores(IEnumerable<WinningLine> winningLines)
        {
            var winningLine = winningLines.FirstOrDefault();

            if (winningLine != null)
            {
                AddVictoryTo(winningLine.Player);
            }          
        }

        private static void AddVictoryTo(Player winner)
        {
            int score;
            var existing = Scores.TryGetValue(winner, out score);
            if (existing)
            {
                Scores[winner] = score + 1;
            }
            else
            {
                Scores.Add(winner, 1);
            };
        }
    }
}
