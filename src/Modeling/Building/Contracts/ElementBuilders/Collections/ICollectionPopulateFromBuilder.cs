namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn>
{
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(string id);
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(Func<TItem, string> idFunc);
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Uri uri);
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Func<TItem, Uri> uriFunc);
}
