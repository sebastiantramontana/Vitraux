using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionTargetBuilder<TItem, TEndCollectionReturn>(CollectionData collectionData, TEndCollectionReturn endCollectionReturn)
    : ICollectionTargetBuilder<TItem, TEndCollectionReturn>
{
    public ITableSelectorBuilder<TItem, TEndCollectionReturn> ToTables
        => new TableSelectorBuilder<TItem, TEndCollectionReturn>(collectionData, endCollectionReturn);

    public IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements
        => new ContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>(collectionData, endCollectionReturn);

    public ICollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new CollectionCustomJsBuilder<TItem, TEndCollectionReturn>(target, collectionData, endCollectionReturn);
    }
}
