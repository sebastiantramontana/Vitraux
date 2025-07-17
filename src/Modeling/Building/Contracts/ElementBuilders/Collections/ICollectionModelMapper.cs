using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface ICollectionModelMapper<TItem, TEndCollectionReturn>
{
    ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> MapValue<TValue>(Func<TItem, TValue> func);
    ICollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TInnerItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func);
    ICollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn> ToOwnMapping { get; }
}
