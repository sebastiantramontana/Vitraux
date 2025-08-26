using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionAddParametersBuilder<TViewModel>(
    ActionTarget actionTarget,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : RootActionSourceFinallizableBuilder<TViewModel>(rootActionSourceBuilder, modelMapper), IRootActionAddParametersBuilder<TViewModel>
{
    private readonly IRootActionSourceBuilder<TViewModel> _rootActionSourceBuilder = rootActionSourceBuilder;
    private readonly IModelMapper<TViewModel> _modelMapper = modelMapper;

    public IRootActionPassValueOrNameSourceBuilder<TViewModel> AddParameters
        => new RootActionPassValueOrNameSourceBuilder<TViewModel>(actionTarget, _rootActionSourceBuilder, _modelMapper);
}
