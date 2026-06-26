namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;

public interface ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn>
    : IInnerCollectionFinallizable<TItem, TEndCollectionReturn>, ICollectionValueMultiTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
}