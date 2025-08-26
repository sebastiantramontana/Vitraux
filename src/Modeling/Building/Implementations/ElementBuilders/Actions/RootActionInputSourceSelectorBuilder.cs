using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionInputSourceSelectorBuilder<TViewModel>(
    ActionTarget actionTarget,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper) : IRootActionInputSourceSelectorBuilder<TViewModel>
{
    public IRootActionInputEventBuilder<TViewModel> ById(string id)
        => SetInputSelector(new ElementIdSelectorString(id));

    public IRootActionInputEventBuilder<TViewModel> ByQuery(string query)
        => SetInputSelector(new ElementQuerySelectorString(query));

    private RootActionInputEventBuilder<TViewModel> SetInputSelector(ElementSelectorBase selector)
    {
        actionTarget.Selector = selector;
        return new RootActionInputEventBuilder<TViewModel>(actionTarget, rootActionSourceBuilder, modelMapper);
    }
}
