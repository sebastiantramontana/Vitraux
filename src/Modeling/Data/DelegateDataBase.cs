namespace Vitraux.Modeling.Data;

internal abstract record class DelegateDataBase<TTarget>(Delegate DataFunc) where TTarget : ITarget
{
    private readonly ICollection<TTarget> _targets = [];
    internal IEnumerable<TTarget> Targets => _targets;
    internal void AddTarget(TTarget target)
        => _targets.Add(target);
}
