using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Cinch;
using Model;
using Model.Strategies;
using Model.Utils;

namespace WPFTicTacToe
{
    public class MatchViewModel : ViewModelBase, ITwoPlayersGame
    {
        private Match match;
        private IEnumerable<Square> highlightedSquares;
        private Player playerInTurn;
        private IEnumerable<Square> squares;
        private PlayerPieceMapping playerPieceMapping;
        private bool isMatchRunning;
        private Player winner;
        private Player firstPlayer;
        private Player secondPlayer;
        private bool isInitialized;
        private int totalGames;
        private int secondPlayerWins;
        private int firstPlayerWins;

        public MatchViewModel()
        {
            HighlightedSquares = new List<Square>();
            PlaceHumanPieceCommand = new SimpleCommand<object, object>(PlaceHumanPiece);
            ResetStatsCommand = new SimpleCommand<object, object>(o => ResetStats());
        }

        public Player FirstPlayer
        {
            get { return firstPlayer; }
            set
            {
                if (firstPlayer != value)
                {
                    ResetStats();
                }
                
                firstPlayer = value;
                NotifyPropertyChanged("FirstPlayer");
            }
        }

        public Player SecondPlayer
        {
            get { return secondPlayer; }
            set
            {
                if (secondPlayer != value)
                {
                    ResetStats();
                }
                
                secondPlayer = value;
                NotifyPropertyChanged("SecondPlayer");
            }
        }

        private void ResetStats()
        {
            TotalGames = 0;
            FirstPlayerWins = 0;
            SecondPlayerWins = 0;
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
        }

        public IEnumerable<Square> HighlightedSquares
        {
            get { return highlightedSquares; }
            set
            {
                highlightedSquares = value;
                NotifyPropertyChanged("HighlightedSquares");
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
                IsInitialized = true;
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
            var firstWinningLine = e.WinningLines.FirstOrDefault();

            var winningLines = e.WinningLines.ToList();

            if (firstWinningLine != null)
            {
                HighlightedSquares = firstWinningLine.Squares;

                var winningLine = winningLines.First();
                Winner = winningLine.Player;

                if (Winner == FirstPlayer)
                {
                    FirstPlayerWins++;
                }
                if (Winner == SecondPlayer)
                {
                    SecondPlayerWins++;
                }
            }

            TotalGames++;
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

        public bool IsInitialized
        {
            get { return isInitialized; }
            set
            {
                isInitialized = value;
                NotifyPropertyChanged("IsInitialized");
            }
        }


        public int FirstPlayerWins
        {
            get { return firstPlayerWins; }
            private set
            {
                firstPlayerWins = value;
                NotifyPropertyChanged("FirstPlayerWins");
            }
        }

        public int SecondPlayerWins
        {
            get { return secondPlayerWins; }
            private set
            {
                secondPlayerWins = value;
                NotifyPropertyChanged("SecondPlayerWins");
            }
        }

        public int TotalGames
        {
            get { return totalGames; }
            private set
            {
                totalGames = value;
                NotifyPropertyChanged("TotalGames");
            }
        }

        public ICommand ResetStatsCommand { get; private set; }
    }
}