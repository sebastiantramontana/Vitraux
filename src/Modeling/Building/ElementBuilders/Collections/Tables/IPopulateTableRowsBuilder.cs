namespace Vitraux.Modeling.Building.ElementBuilders.Collections.Tables;

public interface IPopulateTableRowsBuilder<TItem, TViewModelBack>
{
    ICollectionPopulateFromBuilder<TItem, TViewModelBack> PopulatingRows { get; }
}

