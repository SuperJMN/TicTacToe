using System;
using Model;
using Model.Strategies;

namespace Console
{
    class Program
    {
        private static PlayerPieceMapping playerPieceMapping;
        private static BoardConsoleRenderer renderer;
        private static Match session;

        static void Main()
        {
            session = new Match();

            var strategy = new DefaultComputerStrategy();

            var firstPlayer = new ComputerPlayer("Anytta", strategy);
            firstPlayer.WantToMove += FirstPlayerOnWantToMove;
            //var firstPlayerConsoleConnector = new HumanPlayerConsoleConnector(firstPlayer);
            session.AddChallenger(firstPlayer);

            
            
            var secondPlayer = new HumanPlayer("JMN");
            var secondPlayerConsoleConnector = new HumanPlayerConsoleConnector(secondPlayer);
            session.AddChallenger(secondPlayer);

            InitializeRenderer();

            renderer.Render();

            session.Coordinator.GameEnded += (sender, eventArgs) => ShowGameResults();
            session.Board.PiecePlaced += (sender, handlerArgs) => renderer.Render();

            System.Console.Write("¨¨ The match has started!\n\n");
            session.Start();

            //firstPlayerConsoleConnector.Dispose();
            secondPlayerConsoleConnector.Dispose();
            
            System.Console.ReadLine();
        }

        private static void FirstPlayerOnWantToMove(object player, MoveEventHandlerArgs args)
        {
            System.Console.WriteLine(player + " is making a move!");
        }


        private static void ShowGameResults()
        {
            var hasWinner = session.Board.BoardChecker.HasWinningRow;
            System.Console.WriteLine("The game has ended");

            if (hasWinner)
            {
                System.Console.WriteLine(String.Format("We have a winner! Congratulations, " + session.PlayerInTurn));
            }
            else
            {
                System.Console.WriteLine(String.Format("It's a draw :S"));
            }
        }

        private static void InitializeRenderer()
        {
            InitializeConsoleRenderePieceMapping();
            renderer = new BoardConsoleRenderer(session.Board, playerPieceMapping);
        }

        private static void InitializeConsoleRenderePieceMapping()
        {
            playerPieceMapping = new PlayerPieceMapping();
            playerPieceMapping.Add(session.Contenders[0], 'O');
            playerPieceMapping.Add(session.Contenders[1], 'X');
        }



    }
}
