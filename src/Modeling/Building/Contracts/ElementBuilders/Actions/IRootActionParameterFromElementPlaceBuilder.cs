namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionParameterFromElementPlaceBuilder<TViewModel>
{
    IRootActionParameterNameFinallizableBuilder<TViewModel> FromContent { get; }
    IRootActionParameterNameFinallizableBuilder<TViewModel> FromAttribute(string attribute);
}
