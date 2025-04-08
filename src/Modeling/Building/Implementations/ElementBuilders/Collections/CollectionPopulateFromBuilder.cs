using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionPopulateFromBuilder<TItem, TEndCollectionReturn>(
    CollectionElementTarget collectionElementTarget,
    TEndCollectionReturn endCollectionReturn) 
    : ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(string id)
        => SetInsertionSelector(new TemplateInsertionSelectorId(id));

    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(Func<TItem, string> idFunc)
        => SetInsertionSelector(new TemplateInsertionSelectorDelegate(idFunc));

    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Uri uri)
        => SetInsertionSelector(new UriInsertionSelectorUri(uri));

    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Func<TItem, Uri> uriFunc)
        => SetInsertionSelector(new UriInsertionSelectorDelegate(uriFunc));

    private CollectionModelMapper<TItem, TEndCollectionReturn> SetInsertionSelector(InsertionSelectorBase insertionSelector)
    {
        collectionElementTarget.InsertionSelector = insertionSelector;

        return new CollectionModelMapper<TItem, TEndCollectionReturn>(endCollectionReturn, collectionElementTarget.Data);
    }
}
