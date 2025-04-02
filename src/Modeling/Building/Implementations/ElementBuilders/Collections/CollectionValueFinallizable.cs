using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> : ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>
{
    public TEndCollectionReturn EndCollection { get; }
    public ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> ToElements { get; }
    ModelMappingData ICollectionModelMapper<TItem, TEndCollectionReturn>.Data { get; }

    public ICollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func) => throw new NotImplementedException();
    public ICollectionValueTargetBuilder<TItem, TValue1, TEndCollectionReturn> MapValue<TValue1>(Func<TItem, TValue1> func) => throw new NotImplementedException();
    public ICollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn> ToJsFunction(string jsFunction) => throw new NotImplementedException();
}