using System;

namespace Model
{
    public sealed class MatchCoordinator
    {
        private readonly GameOverChecker gameOverChecker;

        public MatchCoordinator(Match match, GameOverChecker gameOverChecker)
        {            
            Match = match;
            this.gameOverChecker = gameOverChecker;
            this.gameOverChecker.Board = match.Board;            
            
        }

        private Match Match { get; set; }
        
        public void PlayerOnWantToMove(object sender, PositionEventHandlerArgs e)
        {
            var player = (Player)sender;
            var movement = new Movement(e.Position, player);

            if (player == Match.PlayerInTurn && !gameOverChecker.IsGameOver)
            {
                Match.Board.Move(movement);
            }
            else
            {
                throw new InvalidOperationException("The turn should be respected");
            }

            if (!gameOverChecker.IsGameOver)
            {
                Match.SwitchTurn();
                Match.PlayerInTurn.RequestMove(Match.Board);                
            }
            else
            {
                OnGameOver(new GameOverEventArgs(gameOverChecker.WinningLines));
            }
        }

        public event GameOverEventHandler GameOver;

        private void OnGameOver(GameOverEventArgs e)
        {
            var handler = GameOver;
            if (handler != null) handler(this, e);
        }


        public void StartGame()
        {
            Match.PlayerInTurn.RequestMove(Match.Board);
        }
    }
}