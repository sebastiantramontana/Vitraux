namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootParametrizableActionSourceBuilder<TViewModel>
{
    IRootParametrizableActionInputSourceSelectorBuilder<TViewModel> FromInputs { get; }
}
