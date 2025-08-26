namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionParameterFromElementSelectorBuilder<TViewModel>
{
    IRootActionParameterFromElementPlaceBuilder<TViewModel> ById(string id);
    IRootActionParameterFromElementPlaceBuilder<TViewModel> ByQuery(string query);
}
