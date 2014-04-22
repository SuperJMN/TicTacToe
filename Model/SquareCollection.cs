using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model
{
    public class SquareCollection : Collection<Square>
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