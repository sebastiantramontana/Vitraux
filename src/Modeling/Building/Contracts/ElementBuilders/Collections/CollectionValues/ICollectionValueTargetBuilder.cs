using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> : ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionModelMapper<TItem, TEndCollectionReturn> ToOwnMapping { get; }
}
