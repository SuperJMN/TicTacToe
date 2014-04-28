using System.Collections.Generic;
using System.Collections.ObjectModel;

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