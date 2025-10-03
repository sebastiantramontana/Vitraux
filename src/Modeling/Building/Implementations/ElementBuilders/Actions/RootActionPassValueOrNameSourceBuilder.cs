using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionPassValueOrNameSourceBuilder<TViewModel>(
    ActionData actionData,
    IModelMapper<TViewModel> modelMapper)
    : RootActionAddParameterNameBuilder<TViewModel>(actionData, modelMapper),
    IRootActionPassValueOrNameSourceBuilder<TViewModel>
{
    private readonly ActionData _actionData = actionData;
    private readonly IModelMapper<TViewModel> _modelMapper = modelMapper;

    public IRootActionAddParameterNameFinallizableBuilder<TViewModel> AddParameterValue
        => AddInputValueParameter();

    private RootActionAddParameterNameFinallizableBuilder<TViewModel> AddInputValueParameter()
    {
        _actionData.PassInputValueParameter = true;
        return new RootActionAddParameterNameFinallizableBuilder<TViewModel>(this, _modelMapper);
    }
}
