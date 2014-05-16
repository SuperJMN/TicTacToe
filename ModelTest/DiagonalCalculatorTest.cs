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

        [TestMethod]
        public void GetDiagonalPositiveTest3()
        {
            var diagonalCalculator = new DiagonalCalculator(16, 10);
            var positions = diagonalCalculator.GetDiagonalPositive(new Position(15, 9)).ToList();

            var expected = new List<Position>
            {
                new Position(6, 0),
                new Position(7, 1),
                new Position(8, 2),
                new Position(9, 3),
                new Position(10, 4),
                new Position(11, 5),
                new Position(12, 6),
                new Position(13, 7),
                new Position(14, 8),
                new Position(15, 9),
            };

            CollectionAssert.AreEqual(expected, positions);
        }

        [TestMethod]
        public void GetDiagonalNegativeTest3()
        {
            var diagonalCalculator = new DiagonalCalculator(16, 10);
            var positions = diagonalCalculator.GetDiagonalNegative(new Position(15, 0)).ToList();

            var expected = new List<Position>
            {
                new Position(6, 9),
                new Position(7, 8),
                new Position(8, 7),
                new Position(9, 6),
                new Position(10, 5),
                new Position(11, 4),
                new Position(12, 3),
                new Position(13, 2),
                new Position(14, 1),
                new Position(15, 0),
            };

            CollectionAssert.AreEqual(expected, positions);
        }

        [TestMethod]
        public void GetNegativeDiagonalTicTacToe()
        {
            var diagonalCalculator = new DiagonalCalculator(3, 3);
            var positions = diagonalCalculator.GetDiagonalNegative(new Position(1, 1)).ToList();

            var expected = new List<Position>
            {
                new Position(0, 2),
                new Position(1, 1),
                new Position(2, 0),                
            };

            CollectionAssert.AreEqual(expected, positions);
        }

        [TestMethod]
        public void GetPositiveDiagonalTicTacToe()
        {
            var diagonalCalculator = new DiagonalCalculator(3, 3);
            var positions = diagonalCalculator.GetDiagonalPositive(new Position(1, 1)).ToList();

            var expected = new List<Position>
            {
                new Position(0, 0),
                new Position(1, 1),
                new Position(2, 2),                
            };

            CollectionAssert.AreEqual(expected, positions);
        }
    }
}
