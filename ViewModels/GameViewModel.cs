using ReactiveUI;
using System.Windows.Input;
using TicTacToe.Views;

namespace TicTacToe.ViewModels;

public class GameViewModel : ViewModelBase
{
    public static Turn CurrentTurn { get; set; } = Turn.X;
    public ICommand ResetGridCommand { get; }

    private bool _isGameOver = false;
    public bool IsGameOver
    {
        get => _isGameOver;
        set => this.RaiseAndSetIfChanged(ref _isGameOver, value);
    }

    private string _winText = string.Empty;
    public string GameOverText
    {
        get => _winText;
        set => this.RaiseAndSetIfChanged(ref _winText, value);
    }

    public GameViewModel()
    {
        ResetGridCommand = ReactiveCommand.Create(GameView.ResetGrid);
    }

    public static void NextTurn()
    {
        if (CurrentTurn == Turn.X) CurrentTurn = Turn.O;
        else CurrentTurn = Turn.X;
    }

    public static void ChangeType(TicTacToeBoxViewModel box)
    {
        if (CurrentTurn == Turn.X) box.BoxType = BoxType.X;
        else box.BoxType = BoxType.O;

        NextTurn();
    }
}

public enum Turn
{
    X,
    O
}