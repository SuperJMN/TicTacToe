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
        
        private PlayerPieceMapping playerPieceMapping;
        private bool isMatchRunning;
        private Player winner;
        private Player firstPlayer;
        private Player secondPlayer;
        private bool isInitialized;
        private Board board;

        public MatchViewModel()
        {
            HighlightedSquares = new List<Square>();
            PlaceHumanPieceCommand = new SimpleCommand<object, object>(PlaceHumanPiece);
            
            GameStatsViewModel = new GameStatsViewModel();
        }

        public GameStatsViewModel GameStatsViewModel { get; set; }

        public Player FirstPlayer
        {
            get { return firstPlayer; }
            set
            {
                if (firstPlayer != value)
                {
                    GameStatsViewModel.ResetStats();
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
                    GameStatsViewModel.ResetStats();
                }

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

                SetupPieceMapping();

                Board = match.Board;
                Winner = null;

                match.AddChallenger(FirstPlayer);
                match.AddChallenger(SecondPlayer);

                match.TurnChanged += MatchOnTurnChanged;
                match.GameOver += MatchOnGameOver;

                IsMatchRunning = true;

                NotifyPropertyChanged("Match");
            }
        }

        public Board Board
        {
            get { return board; }
            set
            {
                board = value;
                NotifyPropertyChanged("Board");
            }
        }

        private void SetupPieceMapping()
        {
            PlayerPieceMapping = new PlayerPieceMapping();
            PlayerPieceMapping.Add(FirstPlayer, 'X');
            PlayerPieceMapping.Add(SecondPlayer, 'O');
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
                    GameStatsViewModel.FirstPlayerWins++;
                }
                if (Winner == SecondPlayer)
                {
                    GameStatsViewModel.SecondPlayerWins++;
                }
            }

            GameStatsViewModel.TotalGames++;
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
                GameStatsViewModel.IsEnabled = true;
                NotifyPropertyChanged("IsInitialized");
            }
        }
    }
}