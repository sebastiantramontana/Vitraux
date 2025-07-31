namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;

public interface IRootCollectionCustomJsBuilder<TItem, TViewModel> : IRootCollectionTargetBuilder<TItem, TViewModel>
{
    IRootCollectionTargetBuilder<TItem, TViewModel> FromModule(Uri moduleUri);
}
