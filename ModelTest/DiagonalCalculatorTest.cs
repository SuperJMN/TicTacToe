using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Utils;

namespace ModelTest
{
    [TestClass]
    public class DiagonalCalculatorTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var diagonalCalculator = new DiagonalCalculator(6, 6);
            var positions = diagonalCalculator.GetDiagonalPositive(new Position(1, 2), 4);
            var flipped = diagonalCalculator.GetDiagonalNegative(new Position(1, 2), 4);
        }
    }
}
