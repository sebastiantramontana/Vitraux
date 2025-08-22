namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionSourceBuilder<TViewModel>
{
    IRootActionInputSourceSelectorBuilder<TViewModel> FromInputs { get; }
}
