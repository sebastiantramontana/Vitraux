namespace Vitraux.Modeling.Building.ElementBuilders.Collections.Tables;

public interface ITableSelectorBuilder<TItem, TViewModelBack>
{
    IPopulateTableRowsBuilder<TItem, TViewModelBack> ById(string id);
    IPopulateTableRowsBuilder<TItem, TViewModelBack> ById(Func<TItem, string> idFunc);
    IPopulateTableRowsBuilder<TItem, TViewModelBack> ByQuery(string query);
    IPopulateTableRowsBuilder<TItem, TViewModelBack> ByQuery(Func<TItem, string> queryFunc);
}

