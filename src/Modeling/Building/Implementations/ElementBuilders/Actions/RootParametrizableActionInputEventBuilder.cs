using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootParametrizableActionInputEventBuilder<TViewModel>(
    ActionTarget actionTarget,
    IRootParametrizableActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : IRootParametrizableActionInputEventBuilder<TViewModel>
{
    public IRootActionPassValueOrNameSourceBuilder<TViewModel> On(string inputEvent)
    {
        actionTarget.Event = inputEvent;
        return new RootActionPassValueOrNameSourceBuilder<TViewModel>(actionTarget, rootActionSourceBuilder, modelMapper);
    }
}
