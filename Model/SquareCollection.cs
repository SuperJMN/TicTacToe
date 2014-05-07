using System.Collections.Generic;

namespace Model
{
    public class SquareCollection : List<Square>
    {
        public SquareCollection()
        {

        }
        public SquareCollection(IList<Square> squares)
            : base(squares)
        {

        }
    }
}