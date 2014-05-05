using System;
using System.Collections.Generic;

namespace Model
{
    public class GameOverEventArgs : EventArgs
    {
        public GameOverEventArgs(IEnumerable<WinningLine> winningLines)
        {
            WinningLines = winningLines;
        }

        public IEnumerable<WinningLine> WinningLines { get; private set; }
    }
}