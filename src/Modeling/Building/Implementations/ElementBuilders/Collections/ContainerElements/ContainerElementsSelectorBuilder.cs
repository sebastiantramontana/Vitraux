using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;

internal class ContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>(
    CollectionData collectionData,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider)
    : IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ById(string id)
        => SetCollectionElementsTarget(new ElementIdSelectorString(id));

    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ById(Func<TItem, string> idFunc)
        => SetCollectionElementsTarget(new ElementIdSelectorDelegate(idFunc));

    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ByQuery(string query)
        => SetCollectionElementsTarget(new ElementQuerySelectorString(query));

    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ByQuery(Func<TItem, string> queryFunc)
        => SetCollectionElementsTarget(new ElementQuerySelectorDelegate(queryFunc));

    private CollectionPopulateFromBuilder<TItem, TEndCollectionReturn> SetCollectionElementsTarget(ElementSelectorBase elementSelector)
    {
        var target = new CollectionElementTarget(elementSelector);
        collectionData.AddTarget(target);

        return new CollectionPopulateFromBuilder<TItem, TEndCollectionReturn>(collectionData, target, endCollectionReturn, serviceProvider);
    }
}

