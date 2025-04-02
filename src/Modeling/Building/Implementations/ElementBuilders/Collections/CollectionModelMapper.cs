using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionModelMapper<TItem, TEndCollectionReturn> : ICollectionModelMapper<TItem, TEndCollectionReturn>
{
    ModelMappingData ICollectionModelMapper<TItem, TEndCollectionReturn>.Data { get; }

    public ICollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func) => throw new NotImplementedException();
    public ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> MapValue<TValue>(Func<TItem, TValue> func) => throw new NotImplementedException();
}
