using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionAddParameterNameBuilder<TViewModel>(
    ActionData actionData,
    IModelMapper<TViewModel> modelMapper) : IRootActionAddParameterNameBuilder<TViewModel>
{
    private readonly IModelMapper<TViewModel> _modelMapper = modelMapper;

    public IRootActionAddParametersFromBuilder<TViewModel> AddParameter(string paramName)
    {
        var parameter = new ActionParameter(paramName);
        actionData.AddParameter(parameter);

        return new RootActionAddParametersFromBuilder<TViewModel>(parameter, this, _modelMapper);
    }
}
