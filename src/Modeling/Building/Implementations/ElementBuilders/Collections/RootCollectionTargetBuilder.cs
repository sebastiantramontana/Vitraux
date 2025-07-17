using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class RootCollectionTargetBuilder<TItem, TViewModelBack>(
    CollectionData collectionData,
    IModelMapper<TViewModelBack> endCollectionReturn,
    IServiceProvider serviceProvider)
    : ICollectionTargetBuilder<TItem, IModelMapper<TViewModelBack>>
{
    public ITableSelectorBuilder<TItem, IModelMapper<TViewModelBack>> ToTables
        => new TableSelectorBuilder<TItem, IModelMapper<TViewModelBack>>(collectionData, endCollectionReturn, serviceProvider);

    public IContainerElementsSelectorBuilder<TItem, IModelMapper<TViewModelBack>> ToContainerElements
        => new ContainerElementsSelectorBuilder<TItem, IModelMapper<TViewModelBack>>(collectionData, endCollectionReturn, serviceProvider);

    public ICollectionCustomJsBuilder<TItem, IModelMapper<TViewModelBack>> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new RootCollectionCustomJsBuilder<TItem, TViewModelBack>(target, collectionData, endCollectionReturn, serviceProvider);
    }
}
