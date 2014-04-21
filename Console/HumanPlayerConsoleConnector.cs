using System;
using System.IO;
using Model;

namespace Console
{
    class HumanPlayerConsoleConnector : IDisposable
    {
        private readonly HumanPlayer player;

        public HumanPlayerConsoleConnector(HumanPlayer player)
        {
            this.player = player;
            player.MoveRequested += HumanPlayerOnMoveRequested;
        }

        private static void HumanPlayerOnMoveRequested(object sender, EventArgs eventArgs)
        {
            var player = (Player)sender;

            bool isPositionInvalid;
            do
            {
                isPositionInvalid = false;
                System.Console.WriteLine("Enter the position in which " + player + " should move next");

                try
                {
                    var position = GetPosition(System.Console.In);
                    player.MakeMove(new Move(position));
                }
                catch (InvalidPositionException)
                {
                    System.Console.WriteLine("The position you entered is invalid. Please, choose another.");
                    isPositionInvalid = true;
                }
            } while (isPositionInvalid);
        }

        private static Position GetPosition(TextReader input)
        {
            var x = PromptForInteger(input, "Column");
            var y = PromptForInteger(input, "Row");

            return new Position(x, y);
        }

        private static int PromptForInteger(TextReader input, string prompt)
        {
            System.Console.Write(prompt + ": ");
            return GetInteger(input);
        }

        private static int GetInteger(TextReader input)
        {
            bool numberIsInvalid;
            int number;

            do
            {
                var line = input.ReadLine();
                numberIsInvalid = !Int32.TryParse(line, out number);
            } while (numberIsInvalid);

            return number;
        }

        public void Dispose()
        {
            player.MoveRequested -= HumanPlayerOnMoveRequested;
        }
    }
}