using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Model.Utils;

namespace Console
{
    internal class ConsoleMatchDecorator : IMatch
    {
        private readonly HumanPlayerConsoleConnectorFactory humanPlayerConsoleConnectorFactory;

        public ConsoleMatchDecorator(Match decoratedMatch, HumanPlayerConsoleConnectorFactory humanPlayerConsoleConnectorFactory, PlayerPieceMapping pieceMapping)
        {
            HumanPlayerConsoleConnectors = new Collection<HumanPlayerConsoleConnector>();
            this.humanPlayerConsoleConnectorFactory = humanPlayerConsoleConnectorFactory;
            PlayerPieceMapping = pieceMapping;
            DecoratedMatch = decoratedMatch;            
        }


        public Match DecoratedMatch
        {
            get { return decoratedMatch; }
            set
            {
                decoratedMatch = value;
                DecoratedMatch.Started += OnStarted;
                DecoratedMatch.GameOver += OnGameOver;
                
                ConnectToPlayers();
            }
        }

        private void ConnectToPlayers()
        {
            foreach (var contender in Contenders)
            {
                if (contender is ComputerPlayer)
                {
                    ConnectComputerPlayer(contender);
                }
                else
                {
                    ConnectHumarPlayer(contender);
                }
            }
        }

        private void ConnectHumarPlayer(Player contender)
        {
            var connector = humanPlayerConsoleConnectorFactory.CreateConnector((HumanPlayer)contender, PlayerPieceMapping[contender], Board);
            HumanPlayerConsoleConnectors.Add(connector);            
        }

        private void ConnectComputerPlayer(Player contender)
        {
            contender.WantToMove += OnComputerWantToMove;
        }

        private Match decoratedMatch;
        private PlayerPieceMapping PlayerPieceMapping { get; set; }
        private Collection<HumanPlayerConsoleConnector> HumanPlayerConsoleConnectors { get; set; }



        protected void OnStarted(object sender, EventArgs eventArgs)
        {
            var boardWriter = CreateBoardWriter();
            boardWriter.Write(System.Console.Out);

            SubscribeToMatchEvents(DecoratedMatch, boardWriter);

            System.Console.Write(" ¨¨ The match has started!\n\n");
        }

        protected void OnGameOver(object sender, GameOverEventArgs gameOverEventArgs)
        {
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

        private void SubscribeToMatchEvents(Match match, BoardStreamWriter writer)
        {
            match.Coordinator.GameOver += (sender, eventArgs) => ShowGameResults();
            match.PlayerMoved += (sender, handlerArgs) =>
            {
                writer.Write(System.Console.Out);
                System.Console.WriteLine();
            };            
        }

        private void OnComputerWantToMove(object sender, PositionEventHandlerArgs args)
        {
            var player = (Player)sender;
            System.Console.WriteLine(" · {0} ({1}) is moving!", player, PlayerPieceMapping[player]);
        }

        private void ShowGameResults()
        {
            System.Console.WriteLine("The game has ended");

            if (DecoratedMatch.HasWinner)
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

        public Player FirstPlayer
        {
            get { return DecoratedMatch.FirstPlayer; }
        }

        public Player SecondPlayer
        {
            get { return DecoratedMatch.SecondPlayer; }
        }

        private Board Board
        {
            get { return DecoratedMatch.Board; }
        }

        public Player PlayerInTurn
        {
            get { return DecoratedMatch.PlayerInTurn; }
        }

        public bool HasWinner
        {
            get { return DecoratedMatch.HasWinner; }
        }

        public IEnumerable<WinningLine> WinningLines
        {
            get { return DecoratedMatch.WinningLines; }
        }

        public bool IsFinished
        {
            get
            {
                return DecoratedMatch.IsFinished;
            }
        }

        public IList<Player> Contenders
        {
            get { return DecoratedMatch.Contenders; }
        }

        public event MovementEventHandler PlayerMoved;
        public event GameOverEventHandler GameOver;
        public event EventHandler Started;

        public void Start()
        {
            DecoratedMatch.Start();
        }

        public event EventHandler TurnChanged;
    }
}