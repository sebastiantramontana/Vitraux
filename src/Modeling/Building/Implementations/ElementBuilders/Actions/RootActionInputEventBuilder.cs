using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionInputEventBuilder<TViewModel>(
    ActionTarget actionTarget,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper) 
    : IRootActionInputEventBuilder<TViewModel>
{
    public IRootActionAddParametersBuilder<TViewModel> On(string inputEvent)
    {
        actionTarget.Event = inputEvent;
        return new RootActionAddParametersBuilder<TViewModel>(actionTarget, rootActionSourceBuilder, modelMapper);
    }
}
