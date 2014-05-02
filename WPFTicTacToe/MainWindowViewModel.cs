using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Documents;
using System.Windows.Input;
using Cinch;
using MEFedMVVM.ViewModelLocator;
using Model;
using Model.Strategies;
using Model.Strategies.Minimax;
using Model.Utils;

namespace WPFTicTacToe
{
    [ExportViewModel("Main")]
    public class MainWindowViewModel : ViewModelBase
    {
        private Match match;
        private Player player1;
        private Player player2;
        private IEnumerable<Square> squares;
        private PlayerPieceMapping playerPieceMapping;
        private Player playerInTurn;
        private bool isMatchRunning;
        private IEnumerable<SquareCollection> highlightedLines;
        private Player winner;


        private Match Match
        {
            get { return match; }
            set
            {
                match = value;

                var p1 = new HumanPlayer("Anytta");
                var p2 = new ComputerPlayer("JMN") {Strategy = new MinimaxStrategy(match, player2)};

                player1 = p1;
                player2 = p2;

                match.AddChallenger(player1);
                match.AddChallenger(player2);

                PlayerPieceMapping = new PlayerPieceMapping();
                PlayerPieceMapping.Add(player1, 'X');
                PlayerPieceMapping.Add(player2, 'O');

                Squares = match.Board.Squares;
                Winner = null;

                match.TurnChanged += MatchOnTurnChanged;
                match.GameOver += MatchOnGameOver;

                IsMatchRunning = true;

                NotifyPropertyChanged("Match");
            }
        }

        private void MatchOnGameOver(object sender, GameOverEventArgs e)
        {
            IsMatchRunning = false;
            HighlightedLines = e.WinningLines.Select(line => line.Line);
            if (e.WinningLines.Any())
            {
                var winningLine = e.WinningLines.First();
                Winner = winningLine.Player;
            }
        }

        public IEnumerable<SquareCollection> HighlightedLines
        {
            get { return highlightedLines; }
            set
            {
                highlightedLines = value;
                NotifyPropertyChanged("HighlightedLines");
            }
        }

        private void MatchOnTurnChanged(object sender, EventArgs eventArgs)
        {
            PlayerInTurn = Match.PlayerInTurn;
        }

        public Player PlayerInTurn
        {
            get { return playerInTurn; }
            set
            {
                playerInTurn = value;
                NotifyPropertyChanged("PlayerInTurn");
            }
        }

        public IEnumerable<Square> Squares
        {
            get { return squares; }
            set
            {
                squares = value;
                NotifyPropertyChanged("Squares");
            }
        }

        public PlayerPieceMapping PlayerPieceMapping
        {
            get { return playerPieceMapping; }
            set
            {
                playerPieceMapping = value;
                NotifyPropertyChanged("PlayerPieceMapping");
            }
        }

        public ICommand StartMatchCommand { get; private set; }

        public ICommand PlaceHumanPieceCommand
        {
            get;
            private set;
        }

        [ImportingConstructor]
        public MainWindowViewModel()
        {
            IsMatchRunning = false;
            HighlightedLines = new List<SquareCollection>();
            StartMatchCommand = new SimpleCommand<object, object>(o => true, o => StartNewMatch());
            PlaceHumanPieceCommand = new SimpleCommand<object, object>(o => true, PlaceHumanPiece);
            
            StartNewMatch();
        }

        private void StartNewMatch()
        {
            Match = new Match();
            match.Start();
        }

        private void PlaceHumanPiece(object o)
        {
            var square = (SquareViewModel)o;
            try
            {
                match.PlayerInTurn.MakeMove(square.Position);
            }
            catch (InvalidPositionException)
            {

            }
            catch (InvalidOperationException)
            {

            }
        }

        public bool IsMatchRunning
        {
            get { return isMatchRunning; }
            set
            {
                isMatchRunning = value;
                NotifyPropertyChanged("IsMatchRunning");
            }
        }

        public Player Winner
        {
            get { return winner; }
            set
            {
                winner = value;
                NotifyPropertyChanged("Winner");
            }
        }
    }
}