using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn> : ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> FromModule(Uri moduleUri);
}
