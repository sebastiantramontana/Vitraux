using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;

internal class InnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>(
    CollectionData collectionData,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider)
    : IInnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ByQuery(string query)
        => SetCollectionElementsTarget(new ElementQuerySelectorString(query));

    private CollectionPopulateFromBuilder<TItem, TEndCollectionReturn> SetCollectionElementsTarget(ElementSelectorBase elementSelector)
    {
        var target = new CollectionElementTarget(elementSelector);
        collectionData.AddTarget(target);

        return new CollectionPopulateFromBuilder<TItem, TEndCollectionReturn>(collectionData, target, endCollectionReturn, serviceProvider);
    }
}

