using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionParameterFromElementPlaceBuilder<TViewModel>(
    ActionSourceParameter parameter,
    IRootActionParameterNameBuilder<TViewModel> parameterNameBuilder,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : IRootActionParameterFromElementPlaceBuilder<TViewModel>
{
    public IRootActionParameterNameFinallizableBuilder<TViewModel> FromContent
        => SetElementPlace(ContentElementPlace.Instance);

    public IRootActionParameterNameFinallizableBuilder<TViewModel> FromAttribute(string attribute)
        => SetElementPlace(new AttributeElementPlace(attribute));

    private RootActionParameterNameFinallizableBuilder<TViewModel> SetElementPlace(ElementPlace elementPlace)
    {
        parameter.ElementPlace = elementPlace;
        return new RootActionParameterNameFinallizableBuilder<TViewModel>(parameterNameBuilder, rootActionSourceBuilder, modelMapper);
    }
}
