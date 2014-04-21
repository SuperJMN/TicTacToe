using System.Collections.Generic;

namespace Model
{
    public class Board
    {
        const int BoardSizeConst = 3;

        readonly Slot[,] slots = new Slot[BoardSizeConst, BoardSizeConst];
        private readonly BoardChecker boardChecker;


        public Board()
        {
            CreateSlots();
            boardChecker = new BoardChecker(this);
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
                    slots[i, j] = new Slot();
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

        public IEnumerable<Slot> GetRowSlots(int number)
        {
            for (var i = 0; i < BoardSize; i++)
            {
                yield return slots[i, number];
            }
        }

        public IEnumerable<Slot> GetColumnSlots(int number)
        {
            for (var i = 0; i < BoardSize; i++)
            {
                yield return slots[number, i];
            }
        }


        public Slot GetSlot(Position position)
        {
            if (IsValidPosition(position))
            {
                return slots[position.X, position.Y];
            }
            throw new InvalidPositionException(position);
        }

        private bool IsValidPosition(Position position)
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

        public Slot[,] Slots
        {
            get { return slots; }
        }

        public event PieceEventHandler PiecePlaced;

        protected virtual void OnPiecePlaced(PieceEventHandlerArgs args)
        {
            var handler = PiecePlaced;
            if (handler != null) handler(this, args);
        }
    }
}
