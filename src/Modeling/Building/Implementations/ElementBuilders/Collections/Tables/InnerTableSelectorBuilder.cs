using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;

internal class InnerTableSelectorBuilder<TItem, TEndCollectionReturn>(
    CollectionData collectionData,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider)
    : IInnerTableSelectorBuilder<TItem, TEndCollectionReturn>
{
    public IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ByQuery(string query)
        => SetTableTarget(new ElementQuerySelectorString(query));

    private PopulateTableRowsBuilder<TItem, TEndCollectionReturn> SetTableTarget(ElementSelectorBase elementSelector)
    {
        var tableTarget = new CollectionTableTarget(elementSelector);
        collectionData.AddTarget(tableTarget);

        return new PopulateTableRowsBuilder<TItem, TEndCollectionReturn>(collectionData, tableTarget, endCollectionReturn, serviceProvider);
    }
}

