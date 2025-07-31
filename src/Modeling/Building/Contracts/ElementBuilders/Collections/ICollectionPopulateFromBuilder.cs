namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn>
{
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(string id);
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Uri uri);
}
