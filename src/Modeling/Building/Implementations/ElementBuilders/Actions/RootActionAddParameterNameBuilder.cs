using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionAddParameterNameBuilder<TViewModel>(
    ActionTarget actionTarget,
    IRootParametrizableActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper) : IRootActionAddParameterNameBuilder<TViewModel>
{
    private readonly IRootParametrizableActionSourceBuilder<TViewModel> _rootActionSourceBuilder = rootActionSourceBuilder;
    private readonly IModelMapper<TViewModel> _modelMapper = modelMapper;

    public IRootActionAddParametersFromBuilder<TViewModel> AddParameter(string paramName)
    {
        var parameter = new ActionSourceParameter(paramName);
        actionTarget.AddParameter(parameter);

        return new RootActionAddParametersFromBuilder<TViewModel>(parameter, this, _rootActionSourceBuilder, _modelMapper);
    }
}
