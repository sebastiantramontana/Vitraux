using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootParametrizableActionInputSourceSelectorBuilder<TViewModel>(
    ActionTarget actionTarget,
    IRootParametrizableActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper) : IRootParametrizableActionInputSourceSelectorBuilder<TViewModel>
{
    public IRootParametrizableActionInputEventBuilder<TViewModel> ById(string id)
        => SetInputSelector(new ElementIdSelectorString(id));

    public IRootParametrizableActionInputEventBuilder<TViewModel> ByQuery(string query)
        => SetInputSelector(new ElementQuerySelectorString(query));

    private RootParametrizableActionInputEventBuilder<TViewModel> SetInputSelector(ElementSelectorBase selector)
    {
        actionTarget.Selector = selector;
        return new RootParametrizableActionInputEventBuilder<TViewModel>(actionTarget, rootActionSourceBuilder, modelMapper);
    }
}
