namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;

public interface IRootChildrenSelectorBuilder<TViewModel, TValue>
{
    IRootChildrenPlaceBuilder<TViewModel, TValue> ByQuery(string query);
}
