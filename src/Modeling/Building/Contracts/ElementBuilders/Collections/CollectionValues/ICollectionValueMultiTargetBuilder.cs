namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> ToElements { get; }
    ICollectionValueCustomJsFinallizable<TItem, TValue, TEndCollectionReturn> ToJsFunction(string jsFunction);
}
