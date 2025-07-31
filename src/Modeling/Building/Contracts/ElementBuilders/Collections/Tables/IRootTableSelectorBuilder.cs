namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

public interface IRootTableSelectorBuilder<TItem, TViewModel>
{
    IPopulateTableRowsBuilder<TItem, IModelMapper<TViewModel>> ById(string id);
    IPopulateTableRowsBuilder<TItem, IModelMapper<TViewModel>> ByQuery(string query);
}