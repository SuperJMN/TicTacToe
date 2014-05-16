using System;
using System.Collections.Generic;
using Model.Strategies;

namespace Model
{
    public interface IMatch : ITwoPlayersGame
    {
        Player PlayerInTurn { get; }        
        bool HasWinner { get; }
        IEnumerable<WinningLine> WinningLines { get; }
        bool IsFinished { get; }
        IList<Player> Contenders { get; }
        event MovementEventHandler PlayerMoved;
        event GameOverEventHandler GameOver;
        event EventHandler Started;
        
        void Start();
    }
}