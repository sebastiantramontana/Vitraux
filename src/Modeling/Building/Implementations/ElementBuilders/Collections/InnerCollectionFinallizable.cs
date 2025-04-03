using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class InnerCollectionFinallizable<TItemBack, TEndCollectionReturn>(TEndCollectionReturn endCollectionReturn)
    : CollectionModelMapper<TItemBack, TEndCollectionReturn>(endCollectionReturn), IInnerCollectionFinallizable<TItemBack, TEndCollectionReturn>
{
    public TEndCollectionReturn EndCollection { get; } = endCollectionReturn;
}
