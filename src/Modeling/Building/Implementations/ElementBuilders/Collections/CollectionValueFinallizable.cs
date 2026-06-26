using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>(
    TEndCollectionReturn endCollectionReturn,
    IInnerCollectionFinallizable<TItem, TEndCollectionReturn> modelMapperWrapped,
    ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn> multiTargetBuilderWrapped)
    : ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>
{
    public TEndCollectionReturn EndCollection
        => endCollectionReturn;

    public IInnerCollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func)
        => modelMapperWrapped.MapCollection(func);

    public ICollectionValueTargetBuilder<TItem, TValue1, TEndCollectionReturn> MapValue<TValue1>(Func<TItem, TValue1> func)
        => modelMapperWrapped.MapValue(func);

    public IInnerCollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn> ToOwnMapping
        => modelMapperWrapped.ToOwnMapping;

    public IInnerTableSelectorBuilder<TItem, TEndCollectionReturn> ToTables
        => modelMapperWrapped.ToTables;

    public IInnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements
        => modelMapperWrapped.ToContainerElements;

    public IInnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToCollectionJsFunction(string jsFunction)
        => modelMapperWrapped.ToCollectionJsFunction(jsFunction);

    public ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> ToElements
        => multiTargetBuilderWrapped.ToElements;

    public ICollectionValueCustomJsFinallizable<TItem, TValue, TEndCollectionReturn> ToValueJsFunction(string jsFunction)
        => multiTargetBuilderWrapped.ToValueJsFunction(jsFunction);
}