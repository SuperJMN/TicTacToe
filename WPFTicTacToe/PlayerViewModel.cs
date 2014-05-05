using Cinch;

namespace WPFTicTacToe
{
    public class PlayerViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public ComputerStrategy ComputerStrategy { get; set; }
    }
}