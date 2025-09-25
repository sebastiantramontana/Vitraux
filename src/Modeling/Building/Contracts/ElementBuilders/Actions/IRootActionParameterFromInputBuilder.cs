namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionParameterFromInputBuilder<TViewModel>
{
    IRootActionAddParameterNameFinallizableBuilder<TViewModel> ById(string id);
    IRootActionAddParameterNameFinallizableBuilder<TViewModel> ByQuery(string query);
}
