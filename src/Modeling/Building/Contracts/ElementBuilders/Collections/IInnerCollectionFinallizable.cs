namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface IInnerCollectionFinallizable<TItemBack, TEndCollectionReturn> : ICollectionModelMapper<TItemBack, TEndCollectionReturn>
{
    TEndCollectionReturn EndCollection { get; }
}
