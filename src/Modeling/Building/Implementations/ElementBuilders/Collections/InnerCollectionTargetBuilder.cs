using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class InnerCollectionTargetBuilder<TItem, TEndCollectionReturn>(
    CollectionData collectionData,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider)
    : IInnerCollectionTargetBuilder<TItem, TEndCollectionReturn>
{
    public IInnerTableSelectorBuilder<TItem, TEndCollectionReturn> ToTables
        => new InnerTableSelectorBuilder<TItem, TEndCollectionReturn>(collectionData, endCollectionReturn, serviceProvider);

    public IInnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> ToContainerElements
        => new InnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>(collectionData, endCollectionReturn, serviceProvider);

    public IInnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn> ToCollectionJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        collectionData.AddTarget(target);

        return new InnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn>(target, collectionData, endCollectionReturn, serviceProvider);
    }
}
