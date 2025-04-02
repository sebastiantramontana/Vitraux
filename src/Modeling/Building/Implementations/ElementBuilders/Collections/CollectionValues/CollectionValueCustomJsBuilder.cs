using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn> : CollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>, ICollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn>
{
    public ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> FromModule(Uri moduleUri) => throw new NotImplementedException();
}
