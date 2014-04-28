using System;
using Model;
using Model.Strategies;
using Model.Strategies.Minimax;

namespace Console
{
    static class Program
    {
        public static Match Match { get; set; }

        static void Main()
        {
            Match = new Match();

            var firstPlayer = CreateComputerPlayer("Anytta");
            var secondPlayer = CreateComputerPlayer("JMN");

            Match.AddChallenger(firstPlayer);
            Match.AddChallenger(secondPlayer);

            var boardWriter = CreateBoardWriter();
            boardWriter.Write(System.Console.Out);

            SubscribeToMatchEvents(Match, boardWriter);


            System.Console.Write("¨¨ The match has started!\n\n");
            Match.Start();

            System.Console.ReadLine();
        }

        private static void SubscribeToMatchEvents(Match match, BoardStreamWriter writer)
        {
            match.Coordinator.GameEnded += (sender, eventArgs) => ShowGameResults();
            match.Board.PiecePlaced += (sender, handlerArgs) => writer.Write(System.Console.Out);
        }

        private static ComputerPlayer CreateComputerPlayer(string name)
        {
            var firstPlayer = new ComputerPlayer(name);
            firstPlayer.Strategy = new MinimaxStrategy(Match, firstPlayer);
            firstPlayer.WantToMove += ComputerOnWantToMove;
            return firstPlayer;
        }

        private static void ComputerOnWantToMove(object player, MoveEventHandlerArgs args)
        {
            System.Console.WriteLine(player + " is making a move!");
        }


        private static void ShowGameResults()
        {
            var hasWinner = Match.HasWinner;
            System.Console.WriteLine("The game has ended");

            if (hasWinner)
            {
                System.Console.WriteLine(String.Format("We have a winner! Congratulations, " + Match.PlayerInTurn));
            }
            else
            {
                System.Console.WriteLine(String.Format("It's a draw :S"));
            }
        }

        private static BoardStreamWriter CreateBoardWriter()
        {
            CreateConsoleRenderePieceMapping();
            var boardWriter = new BoardStreamWriter(Match.Board, CreateConsoleRenderePieceMapping());
            return boardWriter;
        }

        private static PlayerPieceMapping CreateConsoleRenderePieceMapping()
        {
            var playerPieceMapping = new PlayerPieceMapping();
            playerPieceMapping.Add(Match.Contenders[0], 'X');
            playerPieceMapping.Add(Match.Contenders[1], 'O');
            return playerPieceMapping;
        }   
    }
}
