using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> : ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn>
{
    public ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToContent { get; }

    public ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToAttribute(string attribute) => throw new NotImplementedException();
}
