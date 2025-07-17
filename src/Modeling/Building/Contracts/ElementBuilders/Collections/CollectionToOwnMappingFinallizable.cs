using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

internal class CollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn>(
    CollectionData collectionData,
    ITableSelectorBuilder<TItem, TEndCollectionReturn> toTableWrapped,
    IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> toContainerElementsWrapped,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider) : ICollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn>
{
    public TEndCollectionReturn EndCollection
        => endCollectionReturn;
    public ITableSelectorBuilder<TItem, TEndCollectionReturn> ToTables
        => toTableWrapped;
    public IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements
        => toContainerElementsWrapped;
    public ICollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new CollectionToOwnMappingCustomJsBuilder<TItem, TEndCollectionReturn>(target, collectionData, toTableWrapped, toContainerElementsWrapped, endCollectionReturn, serviceProvider);
    }
}
