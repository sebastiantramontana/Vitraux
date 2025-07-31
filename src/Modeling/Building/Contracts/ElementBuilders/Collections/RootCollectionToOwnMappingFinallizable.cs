using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

internal class RootCollectionToOwnMappingFinallizable<TItem, TViewModel>(
    CollectionData collectionData,
    IRootTableSelectorBuilder<TItem, TViewModel> toTableWrapped,
    IRootContainerElementsSelectorBuilder<TItem, TViewModel> toContainerElementsWrapped,
    IModelMapper<TViewModel> endCollectionReturn,
    IServiceProvider serviceProvider) : IRootCollectionToOwnMappingFinallizable<TItem, TViewModel>
{
    public IModelMapper<TViewModel> EndCollection
        => endCollectionReturn;
    public IRootTableSelectorBuilder<TItem, TViewModel> ToTables
        => toTableWrapped;
    public IRootContainerElementsSelectorBuilder<TItem, TViewModel> ToContainerElements
        => toContainerElementsWrapped;
    public IRootCollectionCustomJsBuilder<TItem, TViewModel> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new RootCollectionToOwnMappingCustomJsBuilder<TItem, TViewModel>(target, collectionData, toTableWrapped, toContainerElementsWrapped, endCollectionReturn, serviceProvider);
    }
}
