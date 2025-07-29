namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToContent { get; }
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToHtml { get; }
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToAttribute(string attribute);
}
