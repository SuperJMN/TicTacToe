using System.Collections.Generic;

namespace Model
{
    public class SquareList : List<Square>
    {
        public SquareList()
        {

        }
        public SquareList(IEnumerable<Square> squares)
            : base(squares)
        {

        }
    }
}