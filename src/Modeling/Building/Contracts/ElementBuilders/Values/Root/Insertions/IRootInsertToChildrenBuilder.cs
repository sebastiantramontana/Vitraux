namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;

public interface IRootInsertToChildrenBuilder<TViewModel, TValue>
{
    IRootChildrenSelectorBuilder<TViewModel, TValue> ToChildren { get; }
}
