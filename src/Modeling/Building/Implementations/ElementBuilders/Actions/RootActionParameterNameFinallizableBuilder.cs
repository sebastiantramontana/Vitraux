using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionParameterNameFinallizableBuilder<TViewModel>(
    IRootActionParameterNameBuilder<TViewModel> rootActionParameterNameBuilder,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : RootActionSourceFinallizableBuilder<TViewModel>(rootActionSourceBuilder, modelMapper), IRootActionParameterNameFinallizableBuilder<TViewModel>
{
    public IRootActionAddParametersFromBuilder<TViewModel> WithName(string paramName)
        => rootActionParameterNameBuilder.WithName(paramName);
}