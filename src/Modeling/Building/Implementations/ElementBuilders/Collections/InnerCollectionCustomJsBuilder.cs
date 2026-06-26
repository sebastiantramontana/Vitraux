using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class InnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn>(
    CustomJsCollectionTarget target,
    CollectionData collectionData,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider)
    : InnerCollectionTargetBuilder<TItem, TEndCollectionReturn>(collectionData, endCollectionReturn, serviceProvider), IInnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn>
{
    public IInnerCollectionTargetBuilder<TItem, TEndCollectionReturn> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }
}
