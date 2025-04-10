using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class InnerCollectionCustomJsBuilder<TItem, TItemBack, TEndCollectionReturnModelMapper>(
    CustomJsCollectionTarget target,
    CollectionData collectionData,
    ICollectionModelMapper<TItemBack, TEndCollectionReturnModelMapper> modelMapper,
    TEndCollectionReturnModelMapper endCollectionReturnModelMapper)
    : InnerCollectionTargetBuilder<TItem, TItemBack, TEndCollectionReturnModelMapper>(collectionData, modelMapper, endCollectionReturnModelMapper), ICollectionCustomJsBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>>
{
    public ICollectionTargetBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }
}
