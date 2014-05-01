using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Input;
using Cinch;
using MEFedMVVM.ViewModelLocator;
using Model;
using Model.Strategies.Minimax;
using Model.Utils;

namespace WPFTicTacToe
{
    [ExportViewModel("Main")]
    public class MainWindowViewModel : ViewModelBase
    {
        private Match match;
        private Player player1;
        private ComputerPlayer player2;
        private IEnumerable<Square> squares;
        private PlayerPieceMapping playerPieceMapping;


        public Match Match
        {
            get { return match; }
            set
            {
                match = value;

                player1 = new HumanPlayer("JMN");
                player2 = new ComputerPlayer("CPU2");
                player2.Strategy=new MinimaxStrategy(match, player2);

                match.AddChallenger(player1);
                match.AddChallenger(player2);

                PlayerPieceMapping = new PlayerPieceMapping();
                PlayerPieceMapping.Add(player1, 'X');
                PlayerPieceMapping.Add(player2, 'O');

                Squares = match.Board.Squares;
                NotifyPropertyChanged("Match");
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
            StartMatchCommand = new SimpleCommand<object, object>(o => true, o =>
            {
                Match = new Match();
                match.Start();
            });
            PlaceHumanPieceCommand = new SimpleCommand<object, object>(o => true, o => PlaceHumanPiece(o));
        }

        private void PlaceHumanPiece(object o)
        {
            var square = (SquareViewModel) o;
            try
            {
                player1.MakeMove(square.Position);
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