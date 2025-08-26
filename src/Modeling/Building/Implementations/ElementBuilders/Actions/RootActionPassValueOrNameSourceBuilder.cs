using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionPassValueOrNameSourceBuilder<TViewModel>(
    ActionTarget actionTarget,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : RootActionParameterNameBuilder<TViewModel>(actionTarget, rootActionSourceBuilder, modelMapper), IRootActionPassValueOrNameSourceBuilder<TViewModel>
{
    private readonly ActionTarget _actionTarget = actionTarget;
    private readonly IRootActionSourceBuilder<TViewModel> _rootActionSourceBuilder = rootActionSourceBuilder;
    private readonly IModelMapper<TViewModel> _modelMapper = modelMapper;

    public IRootActionParameterNameFinallizableBuilder<TViewModel> PassValue
        => AddInputValueParameter();

    private RootActionParameterNameFinallizableBuilder<TViewModel> AddInputValueParameter()
    {
        _actionTarget.AddInputValueParameter();
        return new RootActionParameterNameFinallizableBuilder<TViewModel>(this, _rootActionSourceBuilder, _modelMapper);
    }
}
