using Avalonia.Controls;
using System;
using TicTacToe.ViewModels;

namespace TicTacToe.Views;

public partial class GameView : UserControl
{
    public static TicTacToeBoxView[,] Boxes { get; private set; } = new TicTacToeBoxView[3, 3];

    public GameView()
    {
        InitializeComponent();
        boxesGrid.Initialized += SetupGrid;
    }

    public void SetupGrid(object? _, EventArgs _e)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var box = new TicTacToeBoxView((GameViewModel)DataContext!)
                {
                    Margin = new Avalonia.Thickness(5, 5, 0, 0),
                    Row = i,
                    Column = j,
                };
                boxesGrid.Children.Add(box);
                Boxes[i, j] = box;
            }
        }
    }

    public static void ResetGrid()
    {
        var setGameOver = false;
        foreach (var box in Boxes)
        {
            var dc = (TicTacToeBoxViewModel)box.DataContext!;
            dc.BoxType = BoxType.Empty;
            if (!setGameOver)
            {
                box.GameViewModel.IsGameOver = false;
            }
        }
    }
}
