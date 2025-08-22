namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionInputSourceSelectorBuilder<TViewModel>
{
    IRootActionInputEventBuilder<TViewModel> ById(string id);
    IRootActionInputEventBuilder<TViewModel> ByQuery(string query);
}
