﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.Utils
{
    public class DiagonalCalculator
    {
        private int Width { get; set; }
        private int Height { get; set; }

        public DiagonalCalculator(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public IEnumerable<Position> GetDiagonalPositive(Position start, int length)
        {
            if (!CanGetDiagonal(start, length))
            {
                throw new ArgumentException("Cannot get the diagional with the given parameters");
            }

            var positions = new List<Position>();

            var x = start.X;

            for (int y = start.Y; y < start.Y + length; y++)
            {
                var position = new Position(x, y);
                positions.Add(position);
                x++;
            }

            return positions;
        }

        public IEnumerable<Position> GetDiagonalNegative(Position start, int length)
        {
            var diagonalPositive = GetDiagonalPositive(start, length);
            var flipped = diagonalPositive.Select(position => new Position(position.X, Height - position.Y + start.Y - 1));
            return flipped;
        }

        private bool CanGetDiagonal(Position start, int length)
        {
            return start.X >= 0 && start.Y >= 0
                   && start.X + length - 1 < Width && start.Y + length - 1 < Height;
        }

        public IEnumerable<Position> GetDiagonalPositive(Position position)
        {
            var minDistanceToZero = Math.Min(position.X, position.Y);
            var minDistanceToBounds = Math.Min(Width - position.X -1, Height - position.Y -1);

            var p = new Position(position.X - minDistanceToZero, position.Y - minDistanceToZero);

            var m = minDistanceToZero + minDistanceToBounds;
            for (var i = 0; i <= m; i++)
            {
                yield return p;
                p = new Position(p.X + 1, p.Y + 1);
            }
        }
    }
}