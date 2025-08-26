using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionSourceBuilder<TViewModel>(ActionData actionData, IModelMapper<TViewModel> modelMapper) : IRootActionSourceBuilder<TViewModel>
{
    public IRootActionInputSourceSelectorBuilder<TViewModel> FromInputs
        => BuildActionInput();

    private RootActionInputSourceSelectorBuilder<TViewModel> BuildActionInput()
    {
        var actionTarget = new ActionTarget();
        actionData.AddTarget(actionTarget);

        return new RootActionInputSourceSelectorBuilder<TViewModel>(actionTarget, this, modelMapper);
    }
}
