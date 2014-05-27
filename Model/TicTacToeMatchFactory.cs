using Model.Strategies.Minimax;

namespace Model
{
    public class TicTacToeMatchFactory : MatchFactory
    {
        public override Match CreateMatch(MatchConfiguration configuration)
        {
            var board = new TicTacToeBoard();
            var boardChecker = new GameOverChecker(board, 3);
            
            var match = new Match(board, boardChecker);

            var playerFactory = new PlayerFactory(match, boardChecker, new TicTacToeBoardEvaluator());
            var player1 = playerFactory.CreatePlayer(configuration.Player1.Name, configuration.Player1.PlayerType);
            var player2 = playerFactory.CreatePlayer(configuration.Player2.Name, configuration.Player2.PlayerType);

            match.AddChallenger(player1);
            match.AddChallenger(player2);

            return match;
        }        
    }
}