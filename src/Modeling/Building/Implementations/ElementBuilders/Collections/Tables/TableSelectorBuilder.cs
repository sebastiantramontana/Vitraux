using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;

internal class TableSelectorBuilder<TItem, TEndCollectionReturn>(CollectionData collectionData, TEndCollectionReturn endCollectionReturn)
    : ITableSelectorBuilder<TItem, TEndCollectionReturn>
{
    public IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ById(string id)
        => SetTableTarget(new ElementIdSelectorString(id));

    public IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ById(Func<TItem, string> idFunc)
        => SetTableTarget(new ElementIdSelectorDelegate(idFunc));

    public IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ByQuery(string query)
        => SetTableTarget(new ElementQuerySelectorString(query));

    public IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ByQuery(Func<TItem, string> queryFunc)
        => SetTableTarget(new ElementQuerySelectorDelegate(queryFunc));

    private PopulateTableRowsBuilder<TItem, TEndCollectionReturn> SetTableTarget(ElementSelectorBase elementSelector)
    {
        var tableTarget = new CollectionTableTarget(elementSelector);
        collectionData.AddTarget(tableTarget);

        return new PopulateTableRowsBuilder<TItem, TEndCollectionReturn>(tableTarget, endCollectionReturn);
    }
}

