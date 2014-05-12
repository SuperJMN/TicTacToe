using System;
using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public abstract class Board
    {
        private readonly Square[,] squares;

        private readonly GameOverChecker gameOverChecker;

        protected Board(int width, int height)
        {
            Width = width;
            Height = height;
            squares = new Square[width, height];

            CreateSquares();
            gameOverChecker = new GameOverChecker(this);
        }

        public int Height { get; private set; }

        public int Width { get; private set; }

        protected Board(Board original)
            : this(original.Width, original.Height)
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var square = original.squares[x, y];
                    if (square.Piece != null)
                    {
                        squares[x, y].Piece = square.Piece.Clone();
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
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    squares[x, y] = new Square(new Position(x, y));
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
            for (var i = 0; i < Width; i++)
            {
                yield return squares[i, number];
            }
        }

        private IEnumerable<Square> GetColumnSquares(int number)
        {
            for (var i = 0; i < Height; i++)
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

        private bool IsValidPosition(Position position)
        {
            return EmptyPositions.Contains(position);
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
                for (var y = 0; y < Height; y++)
                {
                    var row = GetRowSquares(y);
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
                for (var x = 0; x < Width; x++)
                {
                    var row = GetColumnSquares(x);
                    rows.Add(new SquareCollection(row.ToList()));
                }
                return rows;
            }
        }

        public IEnumerable<SquareCollection> Diagonals
        {
            get
            {
                var start = new Position(0, 0);

                var diagonalCalculator = new DiagonalCalculator(Width, Height);

                var positivePositions = diagonalCalculator.GetDiagonalPositive(start, 3);
                var negativePositions = diagonalCalculator.GetDiagonalNegative(start, 3);

                var positiveSquares = Squares.Where(square => positivePositions.Contains(square.Position));
                var negativeSquares = Squares.Where(square => negativePositions.Contains(square.Position));

                return new List<SquareCollection> { new SquareCollection(positiveSquares), new SquareCollection(negativeSquares) };
            }
        }

        public abstract Board Clone();

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
}
