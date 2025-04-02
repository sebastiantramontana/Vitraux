using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionTargetBuilder<TItem, TEndCollectionReturn> : ICollectionTargetBuilder<TItem, TEndCollectionReturn>
{
    public ITableSelectorBuilder<TItem, TEndCollectionReturn> ToTables { get; }
    public IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements { get; }

    public ICollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToJsFunction(string jsFunction) => throw new NotImplementedException();
}
