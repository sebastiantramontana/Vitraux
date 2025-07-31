namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

public interface IRootValueElementSelectorBuilder<TViewModel, TValue>
{
    IRootValueElementPlaceBuilder<TViewModel, TValue> ById(string id);
    IRootValueElementPlaceBuilder<TViewModel, TValue> ByQuery(string query);
}