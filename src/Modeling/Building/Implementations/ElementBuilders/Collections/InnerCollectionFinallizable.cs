using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class InnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn>(
    TEndCollectionReturn endCollectionReturn,
    ICollectionModelMapper<TItemBack, TEndCollectionReturn> modelMapperWrapped,
    ICollectionTargetBuilder<TItemActual, IInnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn>> targetBuilderWrapped)
    : IInnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn>
{
    public TEndCollectionReturn EndCollection { get; } = endCollectionReturn;

    public ICollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItemBack, TInnerItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItemBack, IEnumerable<TInnerItem>> func)
        => modelMapperWrapped.MapCollection(func);

    public ICollectionValueTargetBuilder<TItemBack, TValue, TEndCollectionReturn> MapValue<TValue>(Func<TItemBack, TValue> func)
        => modelMapperWrapped.MapValue(func);

    public ICollectionCustomJsBuilder<TItemActual, IInnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn>> ToJsFunction(string jsFunction)
        => targetBuilderWrapped.ToJsFunction(jsFunction);

    public ITableSelectorBuilder<TItemActual, IInnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn>> ToTables
        => targetBuilderWrapped.ToTables;

    public IContainerElementsSelectorBuilder<TItemActual, IInnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn>> ToContainerElements
        => targetBuilderWrapped.ToContainerElements;

    public ICollectionToOwnMappingFinallizable<TItemBack, TEndCollectionReturn> ToOwnMapping
        => modelMapperWrapped.ToOwnMapping;
}
