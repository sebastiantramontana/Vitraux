namespace Vitraux.Modeling.Data.Values;

internal record class ValueData(Delegate ValueFunc)
{
    private readonly List<IValueTarget> _targets = [];

    internal IEnumerable<IValueTarget> Targets => _targets;

    internal void AddTarget(IValueTarget target)
        => _targets.Add(target);
}
