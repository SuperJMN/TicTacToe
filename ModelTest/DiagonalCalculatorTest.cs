using System;
using System.Collections.Generic;
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
        public void GetDiagonalPositiveTest1()
        {
            var diagonalCalculator = new DiagonalCalculator(5, 4);
            var positions = diagonalCalculator.GetDiagonalPositive(new Position(3, 2)).ToList();

            var expected = new List<Position>
            {
                new Position(1, 0),
                new Position(2, 1),
                new Position(3, 2),
                new Position(4, 3),
            };

            CollectionAssert.AreEqual(expected, positions);
        }

        [TestMethod]
        public void GetDiagonalPositiveTest2()
        {
            var diagonalCalculator = new DiagonalCalculator(16, 10);
            var positions = diagonalCalculator.GetDiagonalPositive(new Position(2, 8)).ToList();

            var expected = new List<Position>
            {
                new Position(0, 6),
                new Position(1, 7),
                new Position(2, 8),
                new Position(3, 9),
            };

            CollectionAssert.AreEqual(expected, positions);
        }

        [TestMethod]
        public void GetDiagonalNegativeTest()
        {
            var diagonalCalculator = new DiagonalCalculator(5, 4);
            var positions = diagonalCalculator.GetDiagonalNegative(new Position(3, 2)).ToList();

            var expected = new List<Position>
            {
                new Position(2, 3),
                new Position(3, 2),
                new Position(4, 1),                
            };

            CollectionAssert.AreEqual(expected, positions);
        }

        [TestMethod]
        public void GetDiagonalNegativeTest2()
        {
            var diagonalCalculator = new DiagonalCalculator(5, 4);
            var positions = diagonalCalculator.GetDiagonalNegative(new Position(1, 1)).ToList();

            var expected = new List<Position>
            {
                new Position(0, 2),
                new Position(1, 1),
                new Position(2, 0),                
            };

            CollectionAssert.AreEqual(expected, positions);
        }
    }
}
