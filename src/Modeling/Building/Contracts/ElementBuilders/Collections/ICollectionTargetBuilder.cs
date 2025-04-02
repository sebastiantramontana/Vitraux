using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface ICollectionTargetBuilder<TItem, TEndCollectionReturn>
{
    ITableSelectorBuilder<TItem, TEndCollectionReturn> ToTables { get; }
    IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements { get; }
    ICollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToJsFunction(string jsFunction);
}
