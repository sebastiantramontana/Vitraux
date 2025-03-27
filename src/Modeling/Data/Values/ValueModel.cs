namespace Vitraux.Modeling.Data.Values;

internal record class ValueModel(Delegate ValueFunc)
{
    public IEnumerable<Target<ValueTarget>> Targets { get; set; } = [];
}
