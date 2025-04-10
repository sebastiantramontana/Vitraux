using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class RootCollectionCustomJsBuilder<TItem, TViewModelBack>(
    CustomJsCollectionTarget target,
    CollectionData collectionData,
    IModelMapper<TViewModelBack> endCollectionReturn)
    : RootCollectionTargetBuilder<TItem, TViewModelBack>(collectionData, endCollectionReturn), ICollectionCustomJsBuilder<TItem, IModelMapper<TViewModelBack>>
{
    public ICollectionTargetBuilder<TItem, IModelMapper<TViewModelBack>> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }
}