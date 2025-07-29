using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

public interface IPopulateTableRowsBuilder<TItem, TEndCollectionReturn>
{
    ICollectionPopulateTableBuilder<TItem, TEndCollectionReturn> PopulatingRows { get; }
}

