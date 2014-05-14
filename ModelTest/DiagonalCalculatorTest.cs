using System;
using System.Linq;
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
            var diagonalCalculator = new DiagonalCalculator(5, 4);
            var positions = diagonalCalculator.GetDiagonalPositive(new Position(3, 2)).ToList();
            
        }
    }
}
