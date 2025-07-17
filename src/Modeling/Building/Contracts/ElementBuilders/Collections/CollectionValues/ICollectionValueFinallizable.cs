namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> : ICollectionModelMapper<TItem, TEndCollectionReturn>, ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    TEndCollectionReturn EndCollection { get; }
}