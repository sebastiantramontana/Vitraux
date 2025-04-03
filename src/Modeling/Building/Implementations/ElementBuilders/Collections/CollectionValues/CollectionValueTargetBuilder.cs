using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn>(
    ValueData valueData,
    ICollectionModelMapper<TItem, TEndCollectionReturn> modelMapper,
    TEndCollectionReturn endCollectionReturn)
    : CollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>(valueData, modelMapper, endCollectionReturn), 
      ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    private readonly ValueData _valueData = valueData;
    private readonly ICollectionModelMapper<TItem, TEndCollectionReturn> _modelMapper = modelMapper;

    public ICollectionModelMapper<TItem, TEndCollectionReturn> ToOwnMapping
        => BuildOwnMapping();

    private ICollectionModelMapper<TItem, TEndCollectionReturn> BuildOwnMapping()
    {
        var target = new OwnMappingTarget();
        _valueData.AddTarget(target);

        return _modelMapper;
    }
}
