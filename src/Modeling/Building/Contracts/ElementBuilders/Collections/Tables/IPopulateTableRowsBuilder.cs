using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

public interface IPopulateTableRowsBuilder<TItem, TEndCollectionReturn>
{
    ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> PopulatingRows { get; }
}

