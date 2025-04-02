using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;

internal class PopulateTableRowsBuilder<TItem, TEndCollectionReturn> : IPopulateTableRowsBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> PopulatingRows { get; }
}

