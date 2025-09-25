using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionParameterFromElementPlaceBuilder<TViewModel>(
    ActionSourceParameter parameter,
    IRootActionAddParameterNameBuilder<TViewModel> parameterNameBuilder,
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper)
    : IRootActionParameterFromElementPlaceBuilder<TViewModel>
{
    public IRootActionAddParameterNameFinallizableBuilder<TViewModel> FromContent
        => SetElementPlace(ContentElementPlace.Instance);

    public IRootActionAddParameterNameFinallizableBuilder<TViewModel> FromAttribute(string attribute)
        => SetElementPlace(new AttributeElementPlace(attribute));

    private RootActionAddParameterNameFinallizableBuilder<TViewModel> SetElementPlace(ElementPlace elementPlace)
    {
        parameter.ElementPlace = elementPlace;
        return new RootActionAddParameterNameFinallizableBuilder<TViewModel>(parameterNameBuilder, rootActionSourceBuilder, modelMapper);
    }
}
