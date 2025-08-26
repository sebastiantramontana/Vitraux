﻿using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionParameterFromInputBuilder<TViewModel>(
    ActionSourceParameter parameter,
    IRootActionParameterNameBuilder<TViewModel> parameterNameBuilder,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : IRootActionParameterFromInputBuilder<TViewModel>
{
    public IRootActionParameterNameFinallizableBuilder<TViewModel> ById(string id)
        => SetSelectorToParameter(new ElementIdSelectorString(id));

    public IRootActionParameterNameFinallizableBuilder<TViewModel> ByQuery(string query)
        => SetSelectorToParameter(new ElementQuerySelectorString(query));

    private RootActionParameterNameFinallizableBuilder<TViewModel> SetSelectorToParameter(ElementSelectorBase elementSelector)
    {
        parameter.Selector = elementSelector;
        parameter.ElementPlace = ValuePropertyElementPlace.Instance;

        return new RootActionParameterNameFinallizableBuilder<TViewModel>(parameterNameBuilder, rootActionSourceBuilder, modelMapper);
    }
}
