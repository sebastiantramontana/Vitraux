namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface IInnerCollectionFinallizable<TItem, TEndCollectionReturn>
    : ICollectionModelMapper<TItem, TEndCollectionReturn>, IInnerCollectionTargetBuilder<TItem, TEndCollectionReturn>
{
    TEndCollectionReturn EndCollection { get; }
}
