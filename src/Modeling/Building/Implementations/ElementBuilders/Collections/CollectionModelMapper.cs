using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionModelMapper<TItem, TEndCollectionReturn>(TEndCollectionReturn endCollectionReturn, ModelMappingData modelMappingDataToCollect)
    : ICollectionModelMapper<TItem, TEndCollectionReturn>
{
    public ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> MapValue<TValue>(Func<TItem, TValue> func)
    {
        var newValue = new ValueData(func);
        modelMappingDataToCollect.AddValue(newValue);

        return new CollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn>(newValue, this, endCollectionReturn);
    }

    public ICollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TInnerItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func)
    {
        var newCollection = new CollectionData(func);
        modelMappingDataToCollect.AddCollection(newCollection);

        return new InnerCollectionTargetBuilder<TInnerItem, TItem, TEndCollectionReturn>(newCollection, this, endCollectionReturn);
    }

}
