using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootParametrizableActionInputEventBuilder<TViewModel>(
    ActionTarget actionTarget,
    IModelMapper<TViewModel> modelMapper,
    IRootParametrizableActionSourceBuilder<TViewModel> rootActionSourceBuilder)
    : IRootParametrizableActionInputEventBuilder<TViewModel>
{
    public IRootActionSourceOrParametersBuilder<TViewModel> On(params string[] inputEvents)
    {
        if (inputEvents.Length == 0)
            throw new ArgumentException($"Invalid {nameof(inputEvents)} parameter in On(...) method. The event list cannot be null or empty in a MapAction mapping! ViewModel: {typeof(TViewModel).FullName}");

        actionTarget.Events = inputEvents;
        return new RootActionSourceOrParametersBuilder<TViewModel>(actionTarget.Parent, modelMapper, rootActionSourceBuilder);
    }
}
