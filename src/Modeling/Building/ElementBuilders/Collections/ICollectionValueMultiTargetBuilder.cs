namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> ToElements { get; }
    ICollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn> ToJsFunction(string jsFunction);
}
