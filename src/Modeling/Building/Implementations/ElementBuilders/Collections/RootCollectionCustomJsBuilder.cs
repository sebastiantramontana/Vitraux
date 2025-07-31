using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class RootCollectionCustomJsBuilder<TItem, TViewModelBack>(
    CustomJsCollectionTarget target,
    CollectionData collectionData,
    IModelMapper<TViewModelBack> endCollectionReturn,
    IServiceProvider serviceProvider)
    : RootCollectionTargetBuilder<TItem, TViewModelBack>(collectionData, endCollectionReturn, serviceProvider), IRootCollectionCustomJsBuilder<TItem, TViewModelBack>
{
    public IRootCollectionTargetBuilder<TItem, TViewModelBack> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }
}