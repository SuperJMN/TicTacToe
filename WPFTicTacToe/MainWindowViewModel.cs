using System.ComponentModel.Composition;
using System.Windows.Input;
using Cinch;
using MEFedMVVM.ViewModelLocator;
using Model;

namespace WPFTicTacToe
{
    [ExportViewModel("Main")]
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IMessageBoxService messageBoxService;

        [ImportingConstructor]
        public MainWindowViewModel(IMessageBoxService messageBoxService)
        {
            this.messageBoxService = messageBoxService;
            MatchViewModel = new MatchViewModel();
            SettingsViewModel = new SettingsViewModel();
            StartMatchCommand = new SimpleCommand<object, object>(o => StartNewMatch());
        }

        private void StartNewMatch()
        {
            if (SettingsViewModel.FirstPlayer.Name != SettingsViewModel.SecondPlayer.Name)
            {
                var p1 = CreaterPlayer(SettingsViewModel.FirstPlayer);
                var p2 = CreaterPlayer(SettingsViewModel.SecondPlayer);

                MatchViewModel.FirstPlayer = p1;
                MatchViewModel.SecondPlayer = p2;

                MatchViewModel.StartNewMatch();
            }
            else
            {
                messageBoxService.ShowError("Players should have different names!");
            }
        }

        private Player CreaterPlayer(PlayerViewModel playerViewModel)
        {
            var name = playerViewModel.Name;
            PlayerType playerType;
            if (playerViewModel.IsHuman)
            {
                playerType = PlayerType.Human;
            }
            else
            {
                switch (playerViewModel.ComputerStrategy)
                {
                    case ComputerStrategy.Minimax:
                        playerType = PlayerType.ComputerMinimax;
                        break;
                    case ComputerStrategy.Random:
                        playerType = PlayerType.ComputerRandom;
                        break;
                    default:
                        playerType = PlayerType.ComputerDefault;
                        break;
                }
            }
            var factory = new PlayerFactory(MatchViewModel);
            var player  = factory.CreatePlayer(name, playerType);
            return player;
        }

        public MatchViewModel MatchViewModel { get; private set; }

        public SettingsViewModel SettingsViewModel { get; private set; }

        public ICommand StartMatchCommand { get; private set; }
    }
}