using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;

internal class TableSelectorBuilder<TItem, TEndCollectionReturn> : ITableSelectorBuilder<TItem, TEndCollectionReturn>
{
    public IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ById(string id) => throw new NotImplementedException();
    public IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ById(Func<TItem, string> idFunc) => throw new NotImplementedException();
    public IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ByQuery(string query) => throw new NotImplementedException();
    public IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ByQuery(Func<TItem, string> queryFunc) => throw new NotImplementedException();
}

