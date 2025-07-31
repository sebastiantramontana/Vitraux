using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionPopulateFromBuilder<TItem, TEndCollectionReturn>(
    CollectionData originalCollectionData,
    CollectionElementTarget collectionElementTarget,
    TEndCollectionReturn endCollectionReturn,
    IServiceProvider serviceProvider)
    : ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(string id)
        => SetInsertionSelector(new TemplateInsertionSelectorId(id));

    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Uri uri)
        => SetInsertionSelector(new UriInsertionSelectorUri(uri));

    private CollectionModelMapper<TItem, TEndCollectionReturn> SetInsertionSelector(InsertionSelectorBase insertionSelector)
    {
        collectionElementTarget.InsertionSelector = insertionSelector;

        return new CollectionModelMapper<TItem, TEndCollectionReturn>(endCollectionReturn, collectionElementTarget.Data, originalCollectionData, serviceProvider);
    }
}
