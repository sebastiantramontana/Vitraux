using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class CollectionCustomJsBuilder<TItem, TEndCollectionReturn> : CollectionTargetBuilder<TItem, TEndCollectionReturn>, ICollectionCustomJsBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionTargetBuilder<TItem, TEndCollectionReturn> FromModule(Uri moduleUri) => throw new NotImplementedException();
}
