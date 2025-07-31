namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

public interface IInnerTableSelectorBuilder<TItem, TEndCollectionReturn>
{
    IPopulateTableRowsBuilder<TItem, TEndCollectionReturn> ByQuery(string query);
}

