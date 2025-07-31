using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>(
    TEndCollectionReturn endCollectionReturn,
    ICollectionModelMapper<TItem, TEndCollectionReturn> modelMapperWrapped,
    ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn> multiTargetBuilderWrapped)
    : ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>
{
    public TEndCollectionReturn EndCollection
        => endCollectionReturn;

    public IInnerCollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TInnerItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func)
        => modelMapperWrapped.MapCollection(func);

    public ICollectionValueTargetBuilder<TItem, TValue1, TEndCollectionReturn> MapValue<TValue1>(Func<TItem, TValue1> func)
        => modelMapperWrapped.MapValue(func);

    public ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> ToElements
        => multiTargetBuilderWrapped.ToElements;

    public IInnerCollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn> ToOwnMapping
        => modelMapperWrapped.ToOwnMapping;

    public ICollectionValueCustomJsFinallizable<TItem, TValue, TEndCollectionReturn> ToJsFunction(string jsFunction)
        => multiTargetBuilderWrapped.ToJsFunction(jsFunction);
}