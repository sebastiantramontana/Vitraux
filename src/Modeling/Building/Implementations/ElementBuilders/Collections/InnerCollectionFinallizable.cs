using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class InnerCollectionFinallizable<TItemBack, TEndCollectionReturn> : CollectionModelMapper<TItemBack, TEndCollectionReturn>, IInnerCollectionFinallizable<TItemBack, TEndCollectionReturn>
{
    public TEndCollectionReturn EndCollection { get; }
}
