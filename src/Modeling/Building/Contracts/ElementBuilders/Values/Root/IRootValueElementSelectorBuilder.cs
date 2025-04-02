namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

public interface IRootValueElementSelectorBuilder<TViewModel, TValue>
{
    IRootValueElementPlaceBuilder<TViewModel, TValue> ById(string id);
    IRootValueElementPlaceBuilder<TViewModel, TValue> ById(Func<TValue, string> idFunc);
    IRootValueElementPlaceBuilder<TViewModel, TValue> ById(Func<TViewModel, string> idFunc);
    IRootValueElementPlaceBuilder<TViewModel, TValue> ByQuery(string query);
    IRootValueElementPlaceBuilder<TViewModel, TValue> ByQuery(Func<TValue, string> queryFunc);
    IRootValueElementPlaceBuilder<TViewModel, TValue> ByQuery(Func<TViewModel, string> queryFunc);
}