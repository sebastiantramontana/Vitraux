namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionParameterFromElementSourceBuilder<TViewModel>
{
    IRootActionParameterNameFinallizableBuilder<TViewModel> FromContent { get; }
    IRootActionParameterNameFinallizableBuilder<TViewModel> FromAttribute(string attribute);
}
