using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn> : ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    public ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> ToElements { get; }

    public ICollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn> ToJsFunction(string jsFunction) => throw new NotImplementedException();
}
