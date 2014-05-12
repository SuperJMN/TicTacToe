﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model;
using Model.Utils;

namespace WPFTicTacToe.Controls
{
    public class TicTacToeControl : Control
    {

        static TicTacToeControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TicTacToeControl), new FrameworkPropertyMetadata(typeof(TicTacToeControl)));
        }

        #region PlayerPieceMapping

        public static readonly DependencyProperty PlayerPieceMappingProperty =
            DependencyProperty.Register("PlayerPieceMapping", typeof(PlayerPieceMapping), typeof(TicTacToeControl),
                new FrameworkPropertyMetadata((PlayerPieceMapping)null,
                    new PropertyChangedCallback(OnPlayerPieceMappingChanged)));

        public PlayerPieceMapping PlayerPieceMapping
        {
            get { return (PlayerPieceMapping)GetValue(PlayerPieceMappingProperty); }
            set { SetValue(PlayerPieceMappingProperty, value); }
        }

        private static void OnPlayerPieceMappingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (TicTacToeControl)d;
            var oldPlayerPieceMapping = (PlayerPieceMapping)e.OldValue;
            var newPlayerPieceMapping = target.PlayerPieceMapping;
            target.OnPlayerPieceMappingChanged(oldPlayerPieceMapping, newPlayerPieceMapping);
        }

        protected virtual void OnPlayerPieceMappingChanged(PlayerPieceMapping oldPlayerPieceMapping, PlayerPieceMapping newPlayerPieceMapping)
        {
            if (SquareViewModels == null)
            {
                return;
            }

            foreach (var squareViewModel in SquareViewModels)
            {
                squareViewModel.PlayerPieceMapping = newPlayerPieceMapping;
            }
        }

        #endregion

        #region Board

        public static readonly DependencyProperty BoardProperty =
            DependencyProperty.Register("Board", typeof(Board), typeof(TicTacToeControl),
                new FrameworkPropertyMetadata((Board)null,
                    new PropertyChangedCallback(OnBoardChanged)));

        public Board Board
        {
            get { return (Board)GetValue(BoardProperty); }
            set { SetValue(BoardProperty, value); }
        }

        private static void OnBoardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (TicTacToeControl)d;
            var oldBoard = (Board)e.OldValue;
            var newBoard = target.Board;
            target.OnBoardChanged(oldBoard, newBoard);
        }

        protected virtual void OnBoardChanged(Board oldBoard, Board newBoard)
        {
            Squares = newBoard.Squares;
            RowCount = Board.Width;
            ColumnCount = Board.Height;
        }

        #endregion        

        #region SquareViewModels
        public static readonly DependencyProperty SquareViewModelsProperty =
          DependencyProperty.Register("SquareViewModels", typeof(IEnumerable<SquareViewModel>), typeof(TicTacToeControl),
            new FrameworkPropertyMetadata((IEnumerable<SquareViewModel>)null));

        public IEnumerable<SquareViewModel> SquareViewModels
        {
            get { return (IEnumerable<SquareViewModel>)GetValue(SquareViewModelsProperty); }
            set { SetValue(SquareViewModelsProperty, value); }
        }

        #endregion

        #region HighlightedSquares

        public static readonly DependencyProperty HighlightedSquaresProperty =
            DependencyProperty.Register("HighlightedSquares", typeof(IEnumerable<Square>), typeof(TicTacToeControl),
                new FrameworkPropertyMetadata(new List<Square>(),
                    OnHighlightedLinesChanged));

        private IEnumerable<Square> squares;

        public IEnumerable<Square> HighlightedSquares
        {
            get { return (IEnumerable<Square>)GetValue(HighlightedSquaresProperty); }
            set { SetValue(HighlightedSquaresProperty, value); }
        }

        private static void OnHighlightedLinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (TicTacToeControl)d;
            var oldHighlightedLines = (IEnumerable<Square>)e.OldValue;
            var newHighlightedLines = target.HighlightedSquares;
            target.OnHighlightedLinesChanged(oldHighlightedLines, newHighlightedLines);
        }

        protected virtual void OnHighlightedLinesChanged(IEnumerable<Square> oldHighlightedSquares, IEnumerable<Square> newHighlightedSquares)
        {
            if (SquareViewModels == null)
            {
                return;
            }

            var positions = newHighlightedSquares.Select(square => square.Position);
            var toHighlight = from squareViewModel in SquareViewModels
                              where Contains(positions, squareViewModel)
                              select squareViewModel;

            foreach (var squareViewModel in toHighlight)
            {
                squareViewModel.IsWinning = true;
            }
        }

        private static bool Contains(IEnumerable<Position> positions, SquareViewModel squareViewModel)
        {
            return positions.Contains(squareViewModel.Position);
        }

        #endregion

        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        private IEnumerable<Square> Squares
        {
            get { return squares; }
            set
            {
                squares = value;

                if (value != null)
                {
                    var squareViewModels = squares.Select(square => new SquareViewModel(square, PlayerPieceMapping));
                    SquareViewModels = squareViewModels.ToList();
                }
            }
        }
    }
}
