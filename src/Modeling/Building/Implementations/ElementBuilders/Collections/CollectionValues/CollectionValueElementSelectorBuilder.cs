using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn>(
    ElementValueTarget target,
    ICollectionModelMapper<TItem, TEndCollectionReturn> modelMapper,
    ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn> multiTargetBuilder,
    TEndCollectionReturn endCollectionReturn)
    : ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn>
{
    public ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(string query)
        => SetSelectorToTarget(new ElementQuerySelectorString(query));

    private CollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> SetSelectorToTarget(ElementSelectorBase elementSelector)
    {
        target.Selector = elementSelector;
        return new(target, modelMapper, multiTargetBuilder, endCollectionReturn);
    }
}
