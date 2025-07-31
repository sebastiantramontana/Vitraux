namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface IInnerCollectionCustomJsBuilder<TItem, TEndCollectionReturn> : IInnerCollectionTargetBuilder<TItem, TEndCollectionReturn>
{
    IInnerCollectionTargetBuilder<TItem, TEndCollectionReturn> FromModule(Uri moduleUri);
}
