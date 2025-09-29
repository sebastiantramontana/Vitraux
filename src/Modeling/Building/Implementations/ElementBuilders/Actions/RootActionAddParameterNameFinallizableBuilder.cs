using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionAddParameterNameFinallizableBuilder<TViewModel>(
    IRootActionAddParameterNameBuilder<TViewModel> rootActionParameterNameBuilder,
    IRootParametrizableActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : RootParametrizableActionSourceFinallizableBuilder<TViewModel>(rootActionSourceBuilder, modelMapper), IRootActionAddParameterNameFinallizableBuilder<TViewModel>
{
    public IRootActionAddParametersFromBuilder<TViewModel> AddParameter(string paramName)
        => rootActionParameterNameBuilder.AddParameter(paramName);
}