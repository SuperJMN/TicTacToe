using System.Windows.Input;
using Cinch;

namespace WPFTicTacToe
{
    public class GameStatsViewModel : ViewModelBase
    {
        public GameStatsViewModel()
        {
            ResetStatsCommand = new SimpleCommand<object, object>(o => ResetStats());
        }

        private int firstPlayerWins;
        private int secondPlayerWins;
        private int totalGames;
        private bool isEnabled;

        public int FirstPlayerWins
        {
            get { return firstPlayerWins; }
            set
            {
                firstPlayerWins = value;
                NotifyPropertyChanged("FirstPlayerWins");
            }
        }

        public int SecondPlayerWins
        {
            get { return secondPlayerWins; }
            set
            {
                secondPlayerWins = value;
                NotifyPropertyChanged("SecondPlayerWins");
            }
        }

        public int TotalGames
        {
            get { return totalGames; }
            set
            {
                totalGames = value;
                NotifyPropertyChanged("TotalGames");
            }
        }

        public void ResetStats()
        {
            TotalGames = 0;
            FirstPlayerWins = 0;
            SecondPlayerWins = 0;
        }

        public string FirstPlayerName { get; set; }
        public string SecondPlayerName { get; set; }

        public ICommand ResetStatsCommand { get; private set; }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                NotifyPropertyChanged("IsEnabled");
            }
        }
    }
}