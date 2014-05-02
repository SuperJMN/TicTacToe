using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Strategies.Minimax;
using Model.Utils;

namespace Console
{
    internal class ConsoleMatch : Match
    {
        private PlayerPieceMapping PlayerPieceMapping { get; set; }
        private Collection<HumanPlayerConsoleConnector> HumanPlayerConsoleConnectors { get; set; }

        public ConsoleMatch(MatchConfiguration matchConfiguration)
        {
            HumanPlayerConsoleConnectors = new Collection<HumanPlayerConsoleConnector>();
            PlayerPieceMapping = new PlayerPieceMapping();

            var player1 = CreatePlayer(matchConfiguration.Player1);
            var player2 = CreatePlayer(matchConfiguration.Player2);

            AddChallenger(player1);
            AddChallenger(player2);          
        }

        protected override void OnStarted()
        {
            base.OnStarted();

            var boardWriter = CreateBoardWriter();
            boardWriter.Write(System.Console.Out);

            SubscribeToMatchEvents(this, boardWriter);

            System.Console.Write(" ¨¨ The match has started!\n\n");
        }

        protected override void OnGameOver(GameOverEventArgs e)
        {
            base.OnGameOver(e);

            DisposeHumanConsoleAdapters();

            System.Console.Write(" ¨¨ The match has finished\n\n");
        }


        private void DisposeHumanConsoleAdapters()
        {
            foreach (var humanPlayerConsoleConnector in HumanPlayerConsoleConnectors)
            {
                humanPlayerConsoleConnector.Dispose();
            }
        }

        private Player CreatePlayer(PlayerInfo playerInfo)
        {
            var factory = new PlayerFactory(this);
            var player = factory.CreatePlayer(playerInfo.Name, playerInfo.PlayerType);

            if (playerInfo.PlayerType == PlayerType.Human)
            {                
                var connector = new HumanPlayerConsoleConnector((HumanPlayer) player, playerInfo.Piece);
                HumanPlayerConsoleConnectors.Add(connector);
            }
            else
            {                
                player.WantToMove += ComputerOnWantToMove;
            }

            PlayerPieceMapping.Add(player, playerInfo.Piece);          

            return player;
        }

        private Position GenerateRandomPosition()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var x = random.Next(0, 2);
            var y = random.Next(0, 2);
            return new Position(x, y);
        }

        private void SubscribeToMatchEvents(Match match, BoardStreamWriter writer)
        {
            match.Coordinator.GameOver += (sender, eventArgs) => ShowGameResults();
            match.Board.PiecePlaced += (sender, handlerArgs) =>
            {
                writer.Write(System.Console.Out);
                System.Console.WriteLine();
            };            
        }



        private void ComputerOnWantToMove(object sender, PositionEventHandlerArgs args)
        {
            var player = (Player)sender;
            System.Console.WriteLine(" · {0} ({1}) is moving!", player, PlayerPieceMapping[player]);
        }

        private void ShowGameResults()
        {
            var hasWinner = HasWinner;
            System.Console.WriteLine("The game has ended");

            if (hasWinner)
            {
                System.Console.WriteLine(String.Format("We have a winner! Congratulations, " + PlayerInTurn));
            }
            else
            {
                System.Console.WriteLine(String.Format("It's a draw :S"));
            }
        }

        private BoardStreamWriter CreateBoardWriter()
        {
            
            var boardWriter = new BoardStreamWriter(Board, PlayerPieceMapping);
            return boardWriter;
        }   
    }
}