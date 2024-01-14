using Avalonia.Data.Converters;
using TicTacToe.ViewModels;

namespace TicTacToe.Converter;

public class BoxTypeConverters
{
    public static FuncValueConverter<BoxType?, bool?> IsTypeXConverter { get; } = new FuncValueConverter<BoxType?, bool?>(s =>
    {
        return s == BoxType.X;
    });

    public static FuncValueConverter<BoxType?, bool?> IsTypeOConverter { get; } = new FuncValueConverter<BoxType?, bool?>(s =>
    {
        return s == BoxType.O;
    });
}