using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

internal class InnerCollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn>(
    CollectionData collectionData,
    IInnerTableSelectorBuilder<TItem, TEndCollectionReturn> toTableWrapped,
    IInnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> toContainerElementsWrapped,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider) : IInnerCollectionToOwnMappingFinallizable<TItem, TEndCollectionReturn>
{
    public TEndCollectionReturn EndCollection
        => endCollectionReturn;
    public IInnerTableSelectorBuilder<TItem, TEndCollectionReturn> ToTables
        => toTableWrapped;
    public IInnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements
        => toContainerElementsWrapped;
    public IInnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new InnerCollectionToOwnMappingCustomJsBuilder<TItem, TEndCollectionReturn>(target, collectionData, toTableWrapped, toContainerElementsWrapped, endCollectionReturn, serviceProvider);
    }
}