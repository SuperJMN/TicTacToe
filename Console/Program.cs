using System;
using System.Collections.Generic;
using Model;
using Model.Strategies;
using Model.Strategies.Minimax;
using Model.Utils;

namespace Console
{
    static class Program
    {
        static Dictionary<string, int> scores = new Dictionary<string, int>();

        public static void Main()
        {
            var i = 1000;
            for (var t = 0; t < i; t++)
            {

                var matchConfiguration = new MatchConfiguration
                {
                    Player1 = new PlayerInfo("JMN", PlayerType.ComputerRandom, 'O'),
                    Player2 = new PlayerInfo("Anytta", PlayerType.ComputerRandom, 'X'),
                };

                var match = new ConsoleMatch(matchConfiguration);
                match.Start();

                RegisterScores(match);
            }

            ShowScores(i);
            System.Console.ReadLine();
        }

        private static void ShowScores(int playedGames)
        {
            System.Console.WriteLine("Total matches played: {0}", playedGames);

            var wins = 0;
            foreach (var score in scores)
            {
                wins += score.Value;
                System.Console.WriteLine("{0} won {1} times", score.Key, score.Value);
            }

            var draws = playedGames-wins;
            System.Console.WriteLine("There have been {0} draws", draws);
        }

        private static void RegisterScores(Match match)
        {
            if (match.HasWinner)
            {
                var winner = match.GetWinner();
                int score;
                var existing = scores.TryGetValue(winner.Name, out score);
                if (existing)
                {
                    scores[winner.Name] = score + 1;
                }
                else
                {
                    scores.Add(winner.Name, 1);
                }
            }
        }
    }
}
