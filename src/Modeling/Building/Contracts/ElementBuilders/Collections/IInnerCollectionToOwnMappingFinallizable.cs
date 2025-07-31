using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface IInnerCollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn>
{
    IInnerTableSelectorBuilder<TItem, TEndCollectionReturn> ToTables { get; }
    IInnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements { get; }
    IInnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToJsFunction(string jsFunction);
    TEndCollectionReturn EndCollection { get; }
}
