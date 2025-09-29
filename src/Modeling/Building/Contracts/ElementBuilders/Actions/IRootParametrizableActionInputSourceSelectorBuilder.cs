namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootParametrizableActionInputSourceSelectorBuilder<TViewModel>
{
    IRootParametrizableActionInputEventBuilder<TViewModel> ById(string id);
    IRootParametrizableActionInputEventBuilder<TViewModel> ByQuery(string query);
}
