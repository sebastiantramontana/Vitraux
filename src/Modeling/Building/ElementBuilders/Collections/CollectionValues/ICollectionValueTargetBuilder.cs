namespace Vitraux.Modeling.Building.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> : ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionModelMapper<TItem, TEndCollectionReturn> ToOwnMapping { get; }
}
