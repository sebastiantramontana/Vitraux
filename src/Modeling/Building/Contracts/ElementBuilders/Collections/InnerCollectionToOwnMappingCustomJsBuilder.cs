using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

internal class InnerCollectionToOwnMappingCustomJsBuilder<TItem, TEndCollectionReturn>(
    CustomJsCollectionTarget target,
    CollectionData collectionData,
    IInnerTableSelectorBuilder<TItem, TEndCollectionReturn> toTableWrapped,
    IInnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> toContainerElementsWrapped,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider) : IInnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn>
{
    public IInnerTableSelectorBuilder<TItem, TEndCollectionReturn> ToTables
        => toTableWrapped;
    public IInnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements
        => toContainerElementsWrapped;

    public IInnerCollectionTargetBuilder<TItem, TEndCollectionReturn> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }

    public IInnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new InnerCollectionToOwnMappingCustomJsBuilder<TItem, TEndCollectionReturn>(target, collectionData, toTableWrapped, toContainerElementsWrapped, endCollectionReturn, serviceProvider);
    }
}