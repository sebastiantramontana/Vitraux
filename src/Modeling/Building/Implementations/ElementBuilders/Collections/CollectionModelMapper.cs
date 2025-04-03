using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionModelMapper<TItem, TEndCollectionReturn>(TEndCollectionReturn endCollectionReturn)
    : ICollectionModelMapper<TItem, TEndCollectionReturn>
{
    private readonly List<ValueData> _values = [];
    private readonly List<CollectionData> _collections = [];

    public ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> MapValue<TValue>(Func<TItem, TValue> func)
    {
        var newValue = new ValueData(func);
        _values.Add(newValue);

        return new CollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn>(newValue, this, endCollectionReturn);
    }

    public ICollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func)
    {
        var newCollection = new CollectionData(func);
        _collections.Add(newCollection);

        var finallizable = new InnerCollectionFinallizable<TItem, TEndCollectionReturn>(endCollectionReturn);
        return new CollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TEndCollectionReturn>>(newCollection, finallizable);
    }

    ModelMappingData ICollectionModelMapper<TItem, TEndCollectionReturn>.Build()
        => new(_values, _collections);
}
