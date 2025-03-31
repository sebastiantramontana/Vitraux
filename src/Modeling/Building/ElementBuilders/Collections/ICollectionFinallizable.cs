using Vitraux.Modeling.Building.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> : ICollectionModelMapper<TItem, TEndCollectionReturn>, ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    TEndCollectionReturn EndCollection { get; }
}