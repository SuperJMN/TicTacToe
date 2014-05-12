using System.Collections.Generic;

namespace Model
{
    public class SquareCollection : List<Square>
    {
        public SquareCollection()
        {

        }
        public SquareCollection(IEnumerable<Square> squares)
            : base(squares)
        {

        }
    }
}