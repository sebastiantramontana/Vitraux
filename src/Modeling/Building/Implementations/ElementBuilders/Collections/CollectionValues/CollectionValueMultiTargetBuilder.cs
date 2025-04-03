using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>(
    ValueData valueData,
    ICollectionModelMapper<TItem, TEndCollectionReturn> modelMapper,
    TEndCollectionReturn endCollectionReturn)
    : ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    public ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> ToElements
        => BuildElementTarget();

    public ICollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn> ToJsFunction(string jsFunction)
        => BuildCustomJsTarget(jsFunction);

    private CollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> BuildElementTarget()
    {
        var newTarget = new ElementTarget();
        valueData.AddTarget(newTarget);

        return new(newTarget, modelMapper, this, endCollectionReturn);
    }

    private CollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn> BuildCustomJsTarget(string jsFunction)
    {
        var newTarget = new CustomJsValueTarget(jsFunction);
        valueData.AddTarget(newTarget);

        return new(newTarget, modelMapper, this, endCollectionReturn);
    }
}
