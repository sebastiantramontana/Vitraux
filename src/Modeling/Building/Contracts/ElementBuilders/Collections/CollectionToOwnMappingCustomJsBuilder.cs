using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

internal class CollectionToOwnMappingCustomJsBuilder<TItem, TEndCollectionReturn>(
    CustomJsCollectionTarget target,
    CollectionData collectionData,
    ITableSelectorBuilder<TItem, TEndCollectionReturn> toTableWrapped,
    IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> toContainerElementsWrapped,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider) : ICollectionCustomJsBuilder<TItem, TEndCollectionReturn>
{
    public ITableSelectorBuilder<TItem, TEndCollectionReturn> ToTables
        => toTableWrapped;
    public IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements
        => toContainerElementsWrapped;

    public ICollectionTargetBuilder<TItem, TEndCollectionReturn> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }

    public ICollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new CollectionToOwnMappingCustomJsBuilder<TItem, TEndCollectionReturn>(target, collectionData, toTableWrapped, toContainerElementsWrapped, endCollectionReturn, serviceProvider);
    }
}