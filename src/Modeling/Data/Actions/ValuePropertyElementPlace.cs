namespace Vitraux.Modeling.Data.Actions;

internal record class ValuePropertyElementPlace : ElementPlace
{
    internal static ValuePropertyElementPlace Instance { get; } = new();

    private ValuePropertyElementPlace() { }
}

