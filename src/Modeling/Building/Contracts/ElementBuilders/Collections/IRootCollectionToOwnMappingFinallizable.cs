using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface IRootCollectionToOwnMappingFinallizable<TItem, TViewModel>
{
    IRootTableSelectorBuilder<TItem, TViewModel> ToTables { get; }
    IRootContainerElementsSelectorBuilder<TItem, TViewModel> ToContainerElements { get; }
    IRootCollectionCustomJsBuilder<TItem, TViewModel> ToJsFunction(string jsFunction);
    IModelMapper<TViewModel> EndCollection { get; }
}
