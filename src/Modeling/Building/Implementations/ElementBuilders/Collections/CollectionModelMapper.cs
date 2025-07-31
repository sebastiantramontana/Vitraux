using Microsoft.Extensions.DependencyInjection;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionModelMapper<TItem, TEndCollectionReturn>(
    TEndCollectionReturn endCollectionReturn,
    ModelMappingData modelMappingDataToCollect,
    CollectionData originalCollectionData,
    IServiceProvider serviceProvider)
    : ICollectionModelMapper<TItem, TEndCollectionReturn>
{
    public IInnerCollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn> ToOwnMapping
        => AddToOwnMapping();

    public ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> MapValue<TValue>(Func<TItem, TValue> func)
    {
        var newValue = new ValueData(func);
        modelMappingDataToCollect.AddValue(newValue);

        return new CollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn>(newValue, this, endCollectionReturn);
    }

    public IInnerCollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TInnerItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func)
    {
        var newCollection = new CollectionData(func);
        modelMappingDataToCollect.AddCollection(newCollection);

        return new InnerCollectionTargetBuilder<TInnerItem, TItem, TEndCollectionReturn>(newCollection, this, endCollectionReturn, serviceProvider);
    }

    private InnerCollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn> AddToOwnMapping()
    {
        var mc = serviceProvider.GetRequiredService<IModelConfiguration<TItem>>();
        var modelMapper = serviceProvider.GetRequiredService<IModelMapper<TItem>>();
        var data = mc.ConfigureMapping(modelMapper);

        foreach (var value in data.Values)
        {
            modelMappingDataToCollect.AddValue(value);
        }

        foreach (var coll in data.Collections)
        {
            modelMappingDataToCollect.AddCollection(coll);
        }

        var tableSelectorBuilder = new InnerTableSelectorBuilder<TItem, TEndCollectionReturn>(originalCollectionData, endCollectionReturn, serviceProvider);
        var containerElementsSelectorBuilder = new InnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>(originalCollectionData, endCollectionReturn, serviceProvider);
        return new InnerCollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn>(originalCollectionData, tableSelectorBuilder, containerElementsSelectorBuilder, endCollectionReturn, serviceProvider);
    }
}
