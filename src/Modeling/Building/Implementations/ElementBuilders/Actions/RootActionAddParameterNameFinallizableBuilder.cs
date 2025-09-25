using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionAddParameterNameFinallizableBuilder<TViewModel>(
    IRootActionAddParameterNameBuilder<TViewModel> rootActionParameterNameBuilder,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : RootActionSourceFinallizableBuilder<TViewModel>(rootActionSourceBuilder, modelMapper), IRootActionAddParameterNameFinallizableBuilder<TViewModel>
{
    public IRootActionAddParametersFromBuilder<TViewModel> AddParameter(string paramName)
        => rootActionParameterNameBuilder.AddParameter(paramName);
}