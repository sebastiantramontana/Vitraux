using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;

public interface ICollectionPopulateTableBuilder<TItem, TEndCollectionReturn> : ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn>
{
    ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ToTBody(int tbodyIndex);
}
