namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionParameterFromInputBuilder<TViewModel>
{
    IRootActionParameterNameFinallizableBuilder<TViewModel> ById(string id);
    IRootActionParameterNameFinallizableBuilder<TViewModel> ByQuery(string query);
}
