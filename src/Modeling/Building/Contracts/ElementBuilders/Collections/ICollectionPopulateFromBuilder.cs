namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn>
{
    IInnerCollectionFinallizable<TItem, TEndCollectionReturn> FromTemplate(string id);
    IInnerCollectionFinallizable<TItem, TEndCollectionReturn> FromUri(Uri uri);
}
