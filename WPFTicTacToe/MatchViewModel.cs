using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Cinch;
using Model;
using Model.Strategies;
using Model.Strategies.Minimax;
using Model.Utils;

namespace WPFTicTacToe
{
    public class MatchViewModel : ViewModelBase, ITwoPlayersGame
    {
        private Match match;
        private IEnumerable<SquareCollection> highlightedLines;
        private Player playerInTurn;
        private IEnumerable<Square> squares;
        private PlayerPieceMapping playerPieceMapping;
        private bool isMatchRunning;
        private Player winner;
        private Player firstPlayer;
        private Player secondPlayer;

        public MatchViewModel()
        {
            HighlightedLines = new List<SquareCollection>();           
            PlaceHumanPieceCommand = new SimpleCommand<object, object>(PlaceHumanPiece);                        
        }

        public Player FirstPlayer
        {
            get { return firstPlayer; }
            set
            {
                firstPlayer = value;
                NotifyPropertyChanged("FirstPlayer");
            }
        }

        public Player SecondPlayer
        {
            get { return secondPlayer; }
            set
            {
                secondPlayer = value;
                NotifyPropertyChanged("SecondPlayer");
            }
        }

        private Match Match
        {
            get { return match; }
            set
            {
                if (match != null)
                {
                    CleanUpMatch(match);
                }

                match = value;            
   

                PlayerPieceMapping = new PlayerPieceMapping();
                PlayerPieceMapping.Add(FirstPlayer, 'X');
                PlayerPieceMapping.Add(SecondPlayer, 'O');

                Squares = match.Board.Squares;
                Winner = null;

                match.AddChallenger(FirstPlayer);
                match.AddChallenger(SecondPlayer);

                match.TurnChanged += MatchOnTurnChanged;
                match.GameOver += MatchOnGameOver;

                IsMatchRunning = true;

                NotifyPropertyChanged("Match");
            }
        }

        private void CleanUpMatch(Match toCleanup)
        {
            toCleanup.TurnChanged -= MatchOnTurnChanged;
            toCleanup.GameOver -= MatchOnGameOver;
            toCleanup.Dispose();
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

        

        public ICommand PlaceHumanPieceCommand
        {
            get;
            private set;
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

        private void MatchOnTurnChanged(object sender, EventArgs eventArgs)
        {
            PlayerInTurn = Match.PlayerInTurn;
        }

        public void StartNewMatch()
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
    }
}