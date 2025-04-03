using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.CollectionValues;

internal class CollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn>(
    CustomJsValueTarget target,
    ICollectionModelMapper<TItem, TEndCollectionReturn> modelMapper,
    ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn> multiTargetBuilder,
    TEndCollectionReturn endCollectionReturn)
    : CollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>(endCollectionReturn, modelMapper, multiTargetBuilder), 
      ICollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn>
{
    public ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }
}
