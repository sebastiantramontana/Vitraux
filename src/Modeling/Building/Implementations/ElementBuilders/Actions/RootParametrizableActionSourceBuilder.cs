using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootParametrizableActionSourceBuilder<TViewModel>(ActionData actionData, IModelMapper<TViewModel> modelMapper) : IRootParametrizableActionSourceBuilder<TViewModel>
{
    public IRootParametrizableActionInputSourceSelectorBuilder<TViewModel> FromInputs
        => BuildActionInput();

    private RootParametrizableActionInputSourceSelectorBuilder<TViewModel> BuildActionInput()
    {
        var actionTarget = new ActionTarget(actionData);
        actionData.AddTarget(actionTarget);

        return new RootParametrizableActionInputSourceSelectorBuilder<TViewModel>(actionTarget, this, modelMapper);
    }
}
