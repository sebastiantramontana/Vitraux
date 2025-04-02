using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> : CollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>, ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    public ICollectionModelMapper<TItem, TEndCollectionReturn> ToOwnMapping { get; }
}
