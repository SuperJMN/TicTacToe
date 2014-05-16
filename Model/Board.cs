using System;
using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public abstract class Board
    {
        private readonly Square[,] squares;        

        protected Board(int width, int height)
        {
            Width = width;
            Height = height;
            squares = new Square[width, height];

            CreateSquares();
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
                var areAllTaken = Squares.All(square => square.Piece != null);
                return areAllTaken;
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

            if (SquareIsTaken(square))
            {
                throw new InvalidPositionException(movement.Position);
            }

            var piece = new Piece(movement.Player);
            square.Piece = piece;
            OnPlayerMoved(new MovementEventArgs(movement));
        }

        private static bool SquareIsTaken(Square square)
        {
            return square.Piece != null;
        }

        private IList<Square> GetRowSquares(int number)
        {
            var row = new List<Square>();
            for (var i = 0; i < Width; i++)
            {
                row.Add(squares[i, number]);
            }
            return row;
        }

        private IList<Square> GetColumnSquares(int number)
        {
            var row = new List<Square>();
            for (var i = 0; i < Height; i++)
            {
                row.Add(squares[number, i]);
            }
            return row;
        }

        private Square GetSquare(Position position)
        {
            if (IsInsideBoard(position))
            {
                return squares[position.X, position.Y];
            }
            throw new InvalidPositionException(position);
        }

        public abstract IEnumerable<Position> GetValidMovePositions();

        private bool IsInsideBoard(Position position)
        {
            return position.X >= 0 && position.X < Width &&
                position.Y >= 0 && position.Y < Height;
        }

        public Piece GetPiece(Position p)
        {
            var square = GetSquare(p);
            return square.Piece;
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

        public IList<SquareList> Rows
        {
            get
            {
                var rows = new List<SquareList>();
                for (var y = 0; y < Height; y++)
                {
                    var row = GetRowSquares(y);
                    rows.Add(new SquareList(row.ToList()));
                }
                return rows;
            }
        }

        public IList<SquareList> Columns
        {
            get
            {
                var rows = new List<SquareList>();
                for (var x = 0; x < Width; x++)
                {
                    var row = GetColumnSquares(x);
                    rows.Add(new SquareList(row.ToList()));
                }
                return rows;
            }
        }

        public IEnumerable<SquareList> Diagonals
        {
            get
            {
                var start = new Position(0, 0);

                var diagonalCalculator = new DiagonalCalculator(Width, Height);

                var positivePositions = diagonalCalculator.GetDiagonalPositive(start, 3);
                var negativePositions = diagonalCalculator.GetDiagonalNegative(start, 3);

                var positiveSquares = Squares.Where(square => positivePositions.Contains(square.Position));
                var negativeSquares = Squares.Where(square => negativePositions.Contains(square.Position));

                return new List<SquareList> { new SquareList(positiveSquares), new SquareList(negativeSquares) };
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
    }
}
