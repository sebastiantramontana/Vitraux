namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionAddParametersFromBuilder<TViewModel>
{
    IRootActionParameterFromInputBuilder<TViewModel> FromInputs { get; }
    IRootActionParameterFromElementSelectorBuilder<TViewModel> FromElements { get; }
}
