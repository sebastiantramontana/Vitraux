namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionParameterFromElementBuilder<TViewModel>
{
    IRootActionParameterFromElementSourceBuilder<TViewModel> ById(string id);
    IRootActionParameterFromElementSourceBuilder<TViewModel> ByQuery(string query);
}
