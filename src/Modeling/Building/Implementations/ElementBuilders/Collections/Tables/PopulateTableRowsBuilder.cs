using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;

internal class PopulateTableRowsBuilder<TItem, TEndCollectionReturn>(
    CollectionData originalCollectionData,
    CollectionTableTarget collectionTableTarget,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider)
    : IPopulateTableRowsBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> PopulatingRows
        => new CollectionPopulateFromBuilder<TItem, TEndCollectionReturn>(originalCollectionData, collectionTableTarget, endCollectionReturn, serviceProvider);
}

