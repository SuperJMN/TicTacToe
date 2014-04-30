using System;
using Model;
using Model.Strategies;
using Model.Strategies.Minimax;
using Model.Utils;

namespace Console
{
    static class Program
    {
        public static Match Match { get; set; }
        public static PlayerPieceMapping PlayerPieceMapping { get; set; }


        static void Main()
        {
            Match = new Match();

            var cpu = CreateComputerPlayer("CPU1");
            var human = CreateHumanPlayer("JMN");


            var consoleInputAdapter = new HumanPlayerConsoleConnector(human);

            Match.AddChallenger(human);
            Match.AddChallenger(cpu);

            var boardWriter = CreateBoardWriter();
            boardWriter.Write(System.Console.Out);

            SubscribeToMatchEvents(Match, boardWriter);


            System.Console.Write("¨¨ The match has started!\n\n");
            Match.Start();

            consoleInputAdapter.Dispose();
            System.Console.ReadLine();
        }

        private static Position GenerateRandomPosition()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var x = random.Next(0, 2);
            var y = random.Next(0, 2);
            return new Position(x, y);
        }


        private static void SubscribeToMatchEvents(Match match, BoardStreamWriter writer)
        {
            match.Coordinator.GameEnded += (sender, eventArgs) => ShowGameResults();
            match.Board.PiecePlaced += (sender, handlerArgs) =>
                                       {
                                           writer.Write(System.Console.Out);
                                           System.Console.WriteLine();
                                       };            
        }

        private static ComputerPlayer CreateComputerPlayer(string name)
        {
            var firstPlayer = new ComputerPlayer(name);
            firstPlayer.Strategy = new MinimaxStrategy(Match, firstPlayer);
            firstPlayer.WantToMove += ComputerOnWantToMove;
            return firstPlayer;
        }
        private static HumanPlayer CreateHumanPlayer(string name)
        {
            var firstPlayer = new HumanPlayer(name);
            return firstPlayer;
        }


        private static void ComputerOnWantToMove(object sender, MoveEventHandlerArgs args)
        {
            var player = (Player)sender;
            System.Console.WriteLine(player + "({0}) is moving!", PlayerPieceMapping[player]);
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
            var boardWriter = new BoardStreamWriter(Match.Board, PlayerPieceMapping);
            return boardWriter;
        }

        private static void CreateConsoleRenderePieceMapping()
        {
            PlayerPieceMapping = new PlayerPieceMapping();
            PlayerPieceMapping.Add(Match.Contenders[0], 'O');
            PlayerPieceMapping.Add(Match.Contenders[1], 'X');
        }
    }
}
