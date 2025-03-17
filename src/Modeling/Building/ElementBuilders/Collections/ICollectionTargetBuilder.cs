using Vitraux.Modeling.Building.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.ElementBuilders.Collections.Tables;

namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionTargetBuilder<TItem, TModelMapperBack>
{
    ITableSelectorBuilder<TItem, TModelMapperBack> ToTables { get; }
    IContainerElementsSelectorBuilder<TItem, TModelMapperBack> ToContainerElements { get; }
}

