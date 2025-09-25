using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionAddParameterNameBuilder<TViewModel>(
    ActionTarget actionTarget,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper) : RootActionSourceFinallizableBuilder<TViewModel>(rootActionSourceBuilder, modelMapper),
    IRootActionAddParameterNameBuilder<TViewModel>, IRootActionSourceFinallizableBuilder<TViewModel>
{
    private readonly IRootActionSourceBuilder<TViewModel> _rootActionSourceBuilder = rootActionSourceBuilder;
    private readonly IModelMapper<TViewModel> _modelMapper = modelMapper;

    public IRootActionAddParametersFromBuilder<TViewModel> AddParameter(string paramName)
    {
        var parameter = new ActionSourceParameter(paramName);
        actionTarget.AddParameter(parameter);

        return new RootActionAddParametersFromBuilder<TViewModel>(parameter, this, _rootActionSourceBuilder, _modelMapper);
    }
}
