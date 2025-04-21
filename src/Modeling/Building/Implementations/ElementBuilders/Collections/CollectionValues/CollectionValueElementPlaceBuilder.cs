using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn>(
    ElementValueTarget target,
    ICollectionModelMapper<TItem, TEndCollectionReturn> modelMapper,
    ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn> multiTargetBuilder,
    TEndCollectionReturn endCollectionReturn)
    : ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn>
{
    public ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToContent
        => SetElementPlace(ContentElementPlace.Instance);

    public ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToAttribute(string attribute)
        => SetElementPlace(new AttributeElementPlace(attribute));

    private CollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> SetElementPlace(ElementPlace place)
    {
        target.Place = place;
        return new(endCollectionReturn, modelMapper, multiTargetBuilder);
    }
}
