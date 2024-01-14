using Avalonia.Controls;
using Avalonia.Input;
using System.Linq;
using TicTacToe.ViewModels;

namespace TicTacToe.Views;

public partial class TicTacToeBoxView : UserControl
{
    public GameViewModel GameViewModel { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }

    public TicTacToeBoxView(GameViewModel vm)
    {
        InitializeComponent();
        GameViewModel = vm;
    }

    public void HandleBoxPressed(object sender, PointerPressedEventArgs args)
    {
        if (GameViewModel.IsGameOver)
        {
            return;
        }
        var dc = (TicTacToeBoxViewModel)DataContext!;
        if (dc.BoxType == BoxType.Empty)
        {
            dc.ChangeType();
        }
        if (!GameViewModel.IsGameOver)
        {
            // Three in a row check
            bool isThreeInARow = true;
            for (int i = 0; i < 3; i++)
            {
                var box = (TicTacToeBoxViewModel)GameView.Boxes[Row, i].DataContext!;
                if (TurnCheck(ref isThreeInARow, box))
                {
                    break;
                }
            }
            if (isThreeInARow)
            {
                EndGame(GameViewModel.CurrentTurn);
            }

            // Three in a column check
            bool isThreeInAColumn = true;
            for (int i = 0; i < 3; i++)
            {
                var box = (TicTacToeBoxViewModel)GameView.Boxes[i, Column].DataContext!;
                if (TurnCheck(ref isThreeInAColumn, box))
                {
                    break;
                }
            }
            if (isThreeInAColumn)
            {
                EndGame(GameViewModel.CurrentTurn);
            }

            // All three left to right diagonal check
            bool isDiagonalLeftToRight = true;
            int columnIndexLTR = 0;
            for (int row = 0; row < 3; row++)
            {
                var box = (TicTacToeBoxViewModel)GameView.Boxes[row, columnIndexLTR].DataContext!;
                columnIndexLTR++;
                if (TurnCheck(ref isDiagonalLeftToRight, box))
                {
                    break;
                }
            }
            if (isDiagonalLeftToRight)
            {
                EndGame(GameViewModel.CurrentTurn);
            }

            // All three right to left diagonal check
            bool isDiagonalRightToLeft = true;
            int columnIndexRTL = 2;
            for (int row = 0; row < 3; row++)
            {
                var box = (TicTacToeBoxViewModel)GameView.Boxes[row, columnIndexRTL].DataContext!;
                columnIndexRTL--;
                if (TurnCheck(ref isDiagonalRightToLeft, box))
                {
                    break;
                }
            }
            if (isDiagonalRightToLeft)
            {
                EndGame(GameViewModel.CurrentTurn);
            }

            GameViewModel.NextTurn();
            var emptyBoxes = GameView.Boxes.Cast<TicTacToeBoxView>().Where((b) => ((TicTacToeBoxViewModel)b.DataContext!).BoxType == BoxType.Empty);
            if (!emptyBoxes.Any())
            {
                EndGame(null);
            }
        }
    }

    // Returns a bool to determine whether to break out of the pattern checking loops
    private static bool TurnCheck(ref bool winningBool, TicTacToeBoxViewModel box)
    {
        if (box.BoxType.ToString() != GameViewModel.CurrentTurn.ToString())
        {
            winningBool = false;
            return true;
        }
        return false;
    }

    private void EndGame(Turn? winningTurn)
    {
        GameViewModel.GameOverText = winningTurn == null ? "Game Over!" : $"{winningTurn} Wins!";
        GameViewModel.IsGameOver = true;
    }
}