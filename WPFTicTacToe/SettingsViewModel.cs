using Cinch;

namespace WPFTicTacToe
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {

            FirstPlayer = new PlayerViewModel()
            {
                Name = "JMN",
                IsHuman = true,
                ComputerStrategy = ComputerStrategy.Default,
            };
            SecondPlayer =
                new PlayerViewModel()
                {
                    Name = "Anytta",
                    IsHuman = false,
                    ComputerStrategy = ComputerStrategy.Minimax,
                };
        }

        public PlayerViewModel FirstPlayer { get; set; }
        public PlayerViewModel SecondPlayer { get; set; }
    }
}