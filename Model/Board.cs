﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public class Board
    {
        public const int BoardSize = 3;

        readonly Square[,] squares = new Square[BoardSize, BoardSize];

        private readonly GameOverChecker gameOverChecker;

        public Board()
        {
            CreateSquares();
            gameOverChecker = new GameOverChecker(this);
        }

        private Board(Board original)
            : this()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    var square = original.squares[i, j];
                    if (square.Piece != null)
                    {
                        squares[i, j].Piece = square.Piece.Clone();
                    }
                }
            }
        }

        public bool IsFull
        {
            get
            {
                return gameOverChecker.GetIsFull();
            }
        }

        private void CreateSquares()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    squares[i, j] = new Square(new Position(i, j));
                }
            }
        }

        public void Move(Movement movement)
        {
            if (movement == null)
            {
                throw new ArgumentNullException("movement");
            }            

            var square = GetSquare(movement.Position);

            if (square.Piece != null)
            {
                throw new InvalidPositionException(movement.Position);
            }

            var piece = new Piece(movement.Player);
            square.Piece = piece;
            OnPlayerMoved(new MovementEventArgs(movement));
        }

        private IEnumerable<Square> GetRowSquares(int number)
        {
            for (var i = 0; i < BoardSize; i++)
            {
                yield return squares[i, number];
            }
        }

        private IEnumerable<Square> GetColumnSquares(int number)
        {
            for (var i = 0; i < BoardSize; i++)
            {
                yield return squares[number, i];
            }
        }

        private Square GetSquare(Position position)
        {
            if (IsValidPosition(position))
            {
                return squares[position.X, position.Y];
            }
            throw new InvalidPositionException(position);
        }

        private static bool IsValidPosition(Position position)
        {
            return position.X < BoardSize &&
                position.Y < BoardSize;
        }

        public Piece GetPiece(Position p)
        {
            var square = GetSquare(p);
            return square.Piece;
        }

        private GameOverChecker GameOverChecker
        {
            get { return gameOverChecker; }
        }

        public bool HasWinner
        {
            get { return GameOverChecker.WinningLines.Any(); }
        }

        public IEnumerable<Square> Squares
        {
            get { return squares.Cast<Square>(); }
        }

        public event MovementEventHandler PlayerMoved;

        private void OnPlayerMoved(MovementEventArgs args)
        {
            var handler = PlayerMoved;
            if (handler != null) handler(this, args);
        }

        public IEnumerable<SquareCollection> Rows
        {
            get
            {
                var rows = new List<SquareCollection>();
                for (int i = 0; i < BoardSize; i++)
                {
                    var row = GetRowSquares(i);
                    rows.Add(new SquareCollection(row.ToList()));
                }
                return rows;
            }
        }

        public IEnumerable<SquareCollection> Columns
        {
            get
            {
                var rows = new List<SquareCollection>();
                for (int i = 0; i < BoardSize; i++)
                {
                    var row = GetColumnSquares(i);
                    rows.Add(new SquareCollection(row.ToList()));
                }
                return rows;
            }
        }

        public IEnumerable<SquareCollection> Diagonals
        {
            get
            {
                var diagonal1 = new SquareCollection();
                var diagonal2 = new SquareCollection();
                for (var i = 0; i < BoardSize; i++)
                {
                    var piece1 = GetSquare(new Position(i, i));
                    diagonal1.Add(piece1);

                    var piece2 = GetSquare(new Position(i, BoardSize - i - 1));
                    diagonal2.Add(piece2);
                }

                return new List<SquareCollection> { diagonal1, diagonal2 };
            }
        }

        public Board Clone()
        {
            return new Board(this);
        }

        public IEnumerable<Position> EmptyPositions
        {
            get
            {
                return from square in Squares
                    where square.Piece == null
                    select square.Position;
            }
        }

        public override string ToString()
        {
            var encoder = new BoardToStringEncoder(this);
            return encoder.ToString();
        }

        public IEnumerable<WinningLine> WinningLines
        {
            get
            {
                return GameOverChecker.WinningLines;
            }
        }
    }

    public delegate void MovementEventHandler(object sender, MovementEventArgs args);

    public class MovementEventArgs : EventArgs
    {
        public Movement Movement { get; private set; }

        public MovementEventArgs(Movement movement)
        {
            Movement = movement;
        }
    }
}
