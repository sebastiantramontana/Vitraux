using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionParameterNameBuilder<TViewModel>(
    ActionTarget actionTarget,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper) : IRootActionParameterNameBuilder<TViewModel>
{
    public IRootActionAddParametersFromBuilder<TViewModel> WithName(string paramName)
    {
        var parameter = new ActionSourceParameter(paramName);
        actionTarget.AddParameter(parameter);

        return new RootActionAddParametersFromBuilder<TViewModel>(parameter, this, rootActionSourceBuilder, modelMapper);
    }
}
