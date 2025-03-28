namespace Vitraux.Modeling.Data.Values;

internal record class ValueData(Delegate ValueFunc)
{
    public IEnumerable<ValueTarget> Targets { get; set; } = [];
}
