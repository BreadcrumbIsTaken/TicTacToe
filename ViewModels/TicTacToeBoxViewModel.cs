using ReactiveUI;

namespace TicTacToe.ViewModels;

public class TicTacToeBoxViewModel : ViewModelBase
{
    private BoxType _type;
    public BoxType BoxType
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }

    public TicTacToeBoxViewModel()
    {
        BoxType = BoxType.Empty;
    }

    public void ChangeType()
    {
        if (GameViewModel.CurrentTurn == Turn.X) BoxType = BoxType.X;
        else BoxType = BoxType.O;
    }
}

public enum BoxType
{
    X,
    O,
    Empty
}