namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface IInnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn> 
    : ICollectionModelMapper<TItemBack, TEndCollectionReturn>, ICollectionTargetBuilder<TItemActual, IInnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn>>
{
    TEndCollectionReturn EndCollection { get; }
}
