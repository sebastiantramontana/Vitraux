namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionParameterFromElementPlaceBuilder<TViewModel>
{
    IRootActionAddParameterNameFinallizableBuilder<TViewModel> FromContent { get; }
    IRootActionAddParameterNameFinallizableBuilder<TViewModel> FromAttribute(string attribute);
}
