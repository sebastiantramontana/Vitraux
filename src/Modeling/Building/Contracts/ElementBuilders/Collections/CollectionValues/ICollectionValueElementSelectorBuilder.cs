namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ById(string id);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ById(Func<TValue, string> idFunc);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ById(Func<TItem, string> idFunc);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(string query);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(Func<TValue, string> queryFunc);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(Func<TItem, string> queryFunc);
}
