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
        [ImportingConstructor]
        public MainWindowViewModel()
        {
            MatchViewModel = new MatchViewModel();
            SettingsViewModel = new SettingsViewModel();
            StartMatchCommand = new SimpleCommand<object, object>(o => StartNewMatch());
        }

        private void StartNewMatch()
        {
            var p1 = CreaterPlayer(SettingsViewModel.FirstPlayer);
            var p2 = CreaterPlayer(SettingsViewModel.SecondPlayer);


            MatchViewModel.FirstPlayer = p1;
            MatchViewModel.SecondPlayer = p2;

            MatchViewModel.StartNewMatch();
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

    public class PlayerViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public ComputerStrategy ComputerStrategy { get; set; }
    }

    public enum ComputerStrategy
    {
        Default,
        Random,
        Minimax,
    }
}