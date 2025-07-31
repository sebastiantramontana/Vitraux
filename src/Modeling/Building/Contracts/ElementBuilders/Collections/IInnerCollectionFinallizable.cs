namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface IInnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn> 
    : ICollectionModelMapper<TItemBack, TEndCollectionReturn>, IInnerCollectionTargetBuilder<TItemActual, IInnerCollectionFinallizable<TItemBack, TItemActual, TEndCollectionReturn>>
{
    TEndCollectionReturn EndCollection { get; }
}
