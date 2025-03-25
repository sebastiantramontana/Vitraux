namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> : ICollectionModelMapper<TItem, TEndCollectionReturn>, ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    TEndCollectionReturn EndCollection { get; }
}