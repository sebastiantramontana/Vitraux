namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionAddParametersFromBuilder<TViewModel>
{
    IRootActionParameterFromInputBuilder<TViewModel> FromParamInputs { get; }
    IRootActionParameterFromElementSelectorBuilder<TViewModel> FromParamElements { get; }
}
