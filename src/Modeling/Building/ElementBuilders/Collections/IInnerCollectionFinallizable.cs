namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface IInnerCollectionFinallizable<TItemBack, TEndCollectionReturn> : ICollectionModelMapper<TItemBack, TEndCollectionReturn>
{
    TEndCollectionReturn EndCollection { get; }
}
