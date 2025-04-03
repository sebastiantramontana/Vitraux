using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionCustomJsBuilder<TItem, TEndCollectionReturn>(
    CustomJsCollectionTarget target,
    CollectionData collectionData,
    TEndCollectionReturn endCollectionReturn)
    : CollectionTargetBuilder<TItem, TEndCollectionReturn>(collectionData, endCollectionReturn), ICollectionCustomJsBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionTargetBuilder<TItem, TEndCollectionReturn> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }
}
