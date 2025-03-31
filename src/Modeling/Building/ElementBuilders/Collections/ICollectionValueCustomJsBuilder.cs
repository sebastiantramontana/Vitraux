namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn> : ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> FromModule(Uri moduleUri);
}
