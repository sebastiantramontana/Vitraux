namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionCustomJsBuilder<TItem, TEndCollectionReturn> : ICollectionTargetBuilder<TItem, TEndCollectionReturn>
{
    ICollectionTargetBuilder<TItem, TEndCollectionReturn> FromModule(Uri moduleUri);
}
