
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model;
using Model.Utils;

namespace WPFTicTacToe
{
    public class TicTacToeControl : Control
    {

        static TicTacToeControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TicTacToeControl), new FrameworkPropertyMetadata(typeof(TicTacToeControl)));
        }

        public TicTacToeControl()
        {
            RowCount = Board.BoardSize;
            RowCount = Board.BoardSize;            
        }

        #region PlayerPieceMapping
        public static readonly DependencyProperty PlayerPieceMappingProperty =
          DependencyProperty.Register("PlayerPieceMapping", typeof(PlayerPieceMapping), typeof(TicTacToeControl),
            new FrameworkPropertyMetadata((PlayerPieceMapping)null));

        public PlayerPieceMapping PlayerPieceMapping
        {
            get { return (PlayerPieceMapping)GetValue(PlayerPieceMappingProperty); }
            set { SetValue(PlayerPieceMappingProperty, value); }
        }

        #endregion

        #region Squares

        public static readonly DependencyProperty SquaresProperty =
            DependencyProperty.Register("Squares", typeof(IEnumerable), typeof(TicTacToeControl),
                new FrameworkPropertyMetadata(null,
                    new PropertyChangedCallback(OnSquaresChanged)));

        private IEnumerable<SquareViewModel> squareViewModels;

        public IEnumerable Squares
        {
            get { return (IEnumerable)GetValue(SquaresProperty); }
            set { SetValue(SquaresProperty, value); }
        }

        private static void OnSquaresChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (TicTacToeControl)d;
            var oldSquares = (IEnumerable)e.OldValue;
            var newSquares = target.Squares;
            target.OnSquaresChanged(oldSquares, newSquares);
        }

        protected virtual void OnSquaresChanged(IEnumerable oldSquares, IEnumerable newSquares)
        {
            if (newSquares != null)
            {
                var squares = newSquares.Cast<Square>();
                SquareViewModels = squares.Select(square => new SquareViewModel(square, PlayerPieceMapping));
            }
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



        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
    }
}
