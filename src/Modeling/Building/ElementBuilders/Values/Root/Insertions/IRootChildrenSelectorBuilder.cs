namespace Vitraux.Modeling.Building.ElementBuilders.Values.Root.Insertions;

public interface IRootChildrenSelectorBuilder<TViewModel, TValue>
{
    IRootChildrenPlaceBuilder<TViewModel, TValue> ByQuery(string query);
    IRootChildrenPlaceBuilder<TViewModel, TValue> ByQuery(Func<TValue, string> queryFunc);
    IRootChildrenPlaceBuilder<TViewModel, TValue> ByQuery(Func<TViewModel, string> queryFunc);
}
