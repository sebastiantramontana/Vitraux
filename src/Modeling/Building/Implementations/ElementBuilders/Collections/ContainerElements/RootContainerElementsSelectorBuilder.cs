using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;

internal class RootContainerElementsSelectorBuilder<TItem, TViewModel>(
    CollectionData collectionData,
    IModelMapper<TViewModel> endCollectionReturn,
    IServiceProvider serviceProvider)
    : IRootContainerElementsSelectorBuilder<TItem, TViewModel>
{
    public ICollectionPopulateFromBuilder<TItem, IModelMapper<TViewModel>> ById(string id)
        => SetCollectionElementsTarget(new ElementIdSelectorString(id));

    public ICollectionPopulateFromBuilder<TItem, IModelMapper<TViewModel>> ByQuery(string query)
        => SetCollectionElementsTarget(new ElementQuerySelectorString(query));

    private CollectionPopulateFromBuilder<TItem, IModelMapper<TViewModel>> SetCollectionElementsTarget(ElementSelectorBase elementSelector)
    {
        var target = new CollectionElementTarget(elementSelector);
        collectionData.AddTarget(target);

        return new CollectionPopulateFromBuilder<TItem, IModelMapper<TViewModel>>(collectionData, target, endCollectionReturn, serviceProvider);
    }
}