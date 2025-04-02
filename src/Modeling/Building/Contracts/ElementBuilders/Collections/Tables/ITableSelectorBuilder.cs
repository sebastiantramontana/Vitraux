namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

public interface ITableSelectorBuilder<TItem, TEndCollectionReturn>
{
    IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ById(string id);
    IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ById(Func<TItem, string> idFunc);
    IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ByQuery(string query);
    IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ByQuery(Func<TItem, string> queryFunc);
}

