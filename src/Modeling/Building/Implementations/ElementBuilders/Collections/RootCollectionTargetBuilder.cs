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
    : IRootCollectionTargetBuilder<TItem, TViewModelBack>
{
    public IRootTableSelectorBuilder<TItem, TViewModelBack> ToTables
        => new RootTableSelectorBuilder<TItem, TViewModelBack>(collectionData, endCollectionReturn, serviceProvider);

    public IRootContainerElementsSelectorBuilder<TItem, TViewModelBack> ToContainerElements
        => new RootContainerElementsSelectorBuilder<TItem, TViewModelBack>(collectionData, endCollectionReturn, serviceProvider);

    public IRootCollectionCustomJsBuilder<TItem, TViewModelBack> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new RootCollectionCustomJsBuilder<TItem, TViewModelBack>(target, collectionData, endCollectionReturn, serviceProvider);
    }
}
