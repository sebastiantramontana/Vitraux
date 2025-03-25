namespace Vitraux.Modeling.Building.ElementBuilders.Collections.Tables;

public interface IPopulateTableRowsBuilder<TItem, TEndCollectionReturn>
{
    ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> PopulatingRows { get; }
}

