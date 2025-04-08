namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionData(Delegate CollectionFunc)
{
    private readonly List<ICollectionTarget> _targets = [];

    internal IEnumerable<ICollectionTarget> Targets => _targets;

    internal void AddTarget(ICollectionTarget target)
        => _targets.Add(target);
}
