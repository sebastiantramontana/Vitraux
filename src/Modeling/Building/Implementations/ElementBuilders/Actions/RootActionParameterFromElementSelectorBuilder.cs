using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionParameterFromElementSelectorBuilder<TViewModel>(
    ActionSourceParameter parameter,
    IRootActionParameterNameBuilder<TViewModel> parameterNameBuilder,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : IRootActionParameterFromElementSelectorBuilder<TViewModel>
{
    public IRootActionParameterFromElementPlaceBuilder<TViewModel> ById(string id)
        => SetSelectorToParameter(new ElementIdSelectorString(id));

    public IRootActionParameterFromElementPlaceBuilder<TViewModel> ByQuery(string query)
        => SetSelectorToParameter(new ElementQuerySelectorString(query));

    private RootActionParameterFromElementPlaceBuilder<TViewModel> SetSelectorToParameter(ElementSelectorBase elementSelector)
    {
        parameter.Selector = elementSelector;
        return new RootActionParameterFromElementPlaceBuilder<TViewModel>(parameter, parameterNameBuilder, rootActionSourceBuilder, modelMapper);
    }
}
