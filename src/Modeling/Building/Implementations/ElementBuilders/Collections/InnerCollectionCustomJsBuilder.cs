using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class InnerCollectionCustomJsBuilder<TItem, TItemBack, TEndCollectionReturnModelMapper>(
    CustomJsCollectionTarget target,
    CollectionData collectionData,
    ICollectionModelMapper<TItemBack, TEndCollectionReturnModelMapper> modelMapper,
    TEndCollectionReturnModelMapper endCollectionReturnModelMapper,
    IServiceProvider serviceProvider)
    : InnerCollectionTargetBuilder<TItem, TItemBack, TEndCollectionReturnModelMapper>(collectionData, modelMapper, endCollectionReturnModelMapper, serviceProvider), IInnerCollectionCustomJsBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>>
{
    public IInnerCollectionTargetBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }
}
