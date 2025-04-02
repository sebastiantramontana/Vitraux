using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionPopulateFromBuilder<TItem, TEndCollectionReturn> : ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(string id) => throw new NotImplementedException();
    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(Func<TItem, string> idFunc) => throw new NotImplementedException();
    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Uri uri) => throw new NotImplementedException();
    public ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Func<TItem, Uri> uriFunc) => throw new NotImplementedException();
}
