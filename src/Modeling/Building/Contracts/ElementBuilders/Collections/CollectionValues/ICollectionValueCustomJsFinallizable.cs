namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueCustomJsFinallizable<TItem, TValue, TEndCollectionReturn> : ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> FromModule(Uri moduleUri);
}
