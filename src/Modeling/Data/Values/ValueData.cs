namespace Vitraux.Modeling.Data.Values;

internal record class ValueData(Delegate ValueFunc)
{
    public IEnumerable<Target<ValueTarget>> Targets { get; set; } = [];
}
