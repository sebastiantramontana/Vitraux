using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionSourceOrParametersBuilder<TViewModel>(
    ActionData actionData,
    IModelMapper<TViewModel> modelMapper,
    IRootParametrizableActionSourceBuilder<TViewModel> rootParametrizableActionSourceBuilder)
    : RootActionPassValueOrNameSourceBuilder<TViewModel>(actionData, modelMapper), IRootActionSourceOrParametersBuilder<TViewModel>
{
    public IRootParametrizableActionInputSourceSelectorBuilder<TViewModel> FromInputs
        => rootParametrizableActionSourceBuilder.FromInputs;
}
