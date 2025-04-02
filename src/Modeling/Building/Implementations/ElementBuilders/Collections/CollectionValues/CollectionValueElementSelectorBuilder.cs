using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> : ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn>
{
    public ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ById(string id) => throw new NotImplementedException();
    public ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ById(Func<TValue, string> idFunc) => throw new NotImplementedException();
    public ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ById(Func<TItem, string> idFunc) => throw new NotImplementedException();
    public ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(string query) => throw new NotImplementedException();
    public ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(Func<TValue, string> queryFunc) => throw new NotImplementedException();
    public ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(Func<TItem, string> queryFunc) => throw new NotImplementedException();
}
