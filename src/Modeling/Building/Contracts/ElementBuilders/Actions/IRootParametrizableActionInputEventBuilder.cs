namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootParametrizableActionInputEventBuilder<TViewModel>
{
    IRootActionSourceOrParametersBuilder<TViewModel> On(params string[] inputEvents);
}
