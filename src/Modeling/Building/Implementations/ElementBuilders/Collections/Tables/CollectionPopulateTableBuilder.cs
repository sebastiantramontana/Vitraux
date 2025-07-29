using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;

internal class CollectionPopulateTableBuilder<TItem, TEndCollectionReturn>(
    CollectionData originalCollectionData,
    CollectionTableTarget collectionTableTarget,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider)
    : CollectionPopulateFromBuilder<TItem, TEndCollectionReturn>(
        originalCollectionData,
        collectionTableTarget,
        endCollectionReturn,
        serviceProvider), ICollectionPopulateTableBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ToTBody(int tbodyIndex)
    {
        collectionTableTarget.TBodyIndex = tbodyIndex;
        return this;
    }
}