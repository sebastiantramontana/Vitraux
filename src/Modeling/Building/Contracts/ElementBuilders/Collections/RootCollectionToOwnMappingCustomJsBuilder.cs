using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

internal class RootCollectionToOwnMappingCustomJsBuilder<TItem, TViewModel>(
    CustomJsCollectionTarget target,
    CollectionData collectionData,
    IRootTableSelectorBuilder<TItem, TViewModel> toTableWrapped,
    IRootContainerElementsSelectorBuilder<TItem, TViewModel> toContainerElementsWrapped,
    IModelMapper<TViewModel> endCollectionReturn,
    IServiceProvider serviceProvider) : IRootCollectionCustomJsBuilder<TItem, TViewModel>
{
    public IRootTableSelectorBuilder<TItem, TViewModel> ToTables
        => toTableWrapped;
    public IRootContainerElementsSelectorBuilder<TItem, TViewModel> ToContainerElements
        => toContainerElementsWrapped;

    public IRootCollectionTargetBuilder<TItem, TViewModel> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }

    public IRootCollectionCustomJsBuilder<TItem, TViewModel> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new RootCollectionToOwnMappingCustomJsBuilder<TItem, TViewModel>(target, collectionData, toTableWrapped, toContainerElementsWrapped, endCollectionReturn, serviceProvider);
    }
}
