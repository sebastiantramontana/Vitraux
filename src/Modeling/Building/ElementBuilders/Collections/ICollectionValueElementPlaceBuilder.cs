namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToContent { get; }
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToAttribute(string attribute);
}
