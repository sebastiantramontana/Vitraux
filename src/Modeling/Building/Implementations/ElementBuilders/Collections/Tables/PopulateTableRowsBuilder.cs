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
    public ICollectionPopulateTableBuilder<TItem, TEndCollectionReturn> PopulatingRows
        => new CollectionPopulateTableBuilder<TItem, TEndCollectionReturn>(originalCollectionData, collectionTableTarget, endCollectionReturn, serviceProvider);
}