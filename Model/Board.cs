using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Board
    {
        const int BoardSizeConst = 3;

        readonly Square[,] squares = new Square[BoardSizeConst, BoardSizeConst];

        private readonly BoardChecker boardChecker;


        public Board()
        {
            CreateSlots();
            boardChecker = new BoardChecker(this);
        }

        private Board(Board original)
        {
            squares = (Square[,]) original.squares.Clone();            
        }

        public bool IsFull
        {
            get
            {
                return boardChecker.GetIsFull();
            }
        }

        private void CreateSlots()
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    squares[i, j] = new Square();
                }
            }
        }

        public void Move(Player player, Move move)
        {
            var slot = GetSlot(move.Position);
            if (slot.Piece != null)
            {
                throw new InvalidPositionException(move.Position);
            }
            var piece = new Piece(player);
            slot.Piece = piece;
            OnPiecePlaced(new PieceEventHandlerArgs(piece));
        }

        private IEnumerable<Square> GetRowSlots(int number)
        {
            for (var i = 0; i < BoardSize; i++)
            {
                yield return squares[i, number];
            }
        }

        private IEnumerable<Square> GetColumnSlots(int number)
        {
            for (var i = 0; i < BoardSize; i++)
            {
                yield return squares[number, i];
            }
        }


        private Square GetSlot(Position position)
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
            var slot = GetSlot(p);
            return slot.Piece;
        }

        public static int BoardSize
        {
            get
            {
                return BoardSizeConst;
            }
        }

        public BoardChecker BoardChecker
        {
            get { return boardChecker; }
        }

        public IEnumerable<Square> Squares
        {
            get { return squares.Cast<Square>(); }
        }

        public event PieceEventHandler PiecePlaced;

        protected virtual void OnPiecePlaced(PieceEventHandlerArgs args)
        {
            var handler = PiecePlaced;
            if (handler != null) handler(this, args);
        }

        public IEnumerable<SquareCollection> Rows
        {
            get
            {
                var rows = new List<SquareCollection>();
                for (int i = 0; i < BoardSize; i++)
                {
                    var row = GetRowSlots(i);
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
                    var row = GetColumnSlots(i);
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
                    var piece1 = GetSlot(new Position(i, i));
                    diagonal1.Add(piece1);

                    var piece2 = GetSlot(new Position(i, BoardSize - i - 1));
                    diagonal2.Add(piece2);
                }

                return new List<SquareCollection> { diagonal1, diagonal2 };
            }
        }

        public Board Clone()
        {
            return new Board(this);
        }
    }
}
