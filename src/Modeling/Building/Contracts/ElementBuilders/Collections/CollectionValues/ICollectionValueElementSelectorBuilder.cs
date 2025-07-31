namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(string query);
}
