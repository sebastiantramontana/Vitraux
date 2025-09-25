using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionAddParametersFromBuilder<TViewModel>(
    ActionSourceParameter parameter,
    IRootActionAddParameterNameBuilder<TViewModel> parameterNameBuilder,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : IRootActionAddParametersFromBuilder<TViewModel>
{
    public IRootActionParameterFromInputBuilder<TViewModel> FromParamInputs
        => new RootActionParameterFromInputBuilder<TViewModel>(parameter, parameterNameBuilder, rootActionSourceBuilder, modelMapper);

    public IRootActionParameterFromElementSelectorBuilder<TViewModel> FromParamElements
        => new RootActionParameterFromElementSelectorBuilder<TViewModel>(parameter, parameterNameBuilder, rootActionSourceBuilder, modelMapper);
}
