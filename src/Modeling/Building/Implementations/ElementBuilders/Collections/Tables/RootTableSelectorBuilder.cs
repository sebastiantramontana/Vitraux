using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;

internal class RootTableSelectorBuilder<TItem, TViewModel>(
    CollectionData collectionData,
    IModelMapper<TViewModel> endCollectionReturn,
    IServiceProvider serviceProvider)
    : IRootTableSelectorBuilder<TItem, TViewModel>
{
    public IPopulateTableRowsBuilder<TItem, IModelMapper<TViewModel>> ById(string id)
        => SetTableTarget(new ElementIdSelectorString(id));

    public IPopulateTableRowsBuilder<TItem, IModelMapper<TViewModel>> ByQuery(string query)
        => SetTableTarget(new ElementQuerySelectorString(query));

    private PopulateTableRowsBuilder<TItem, IModelMapper<TViewModel>> SetTableTarget(ElementSelectorBase elementSelector)
    {
        var tableTarget = new CollectionTableTarget(elementSelector);
        collectionData.AddTarget(tableTarget);

        return new PopulateTableRowsBuilder<TItem, IModelMapper<TViewModel>>(collectionData, tableTarget, endCollectionReturn, serviceProvider);
    }
}