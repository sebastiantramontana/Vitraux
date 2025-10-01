namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootParametrizableActionInputEventBuilder<TViewModel>
{
    IRootActionSourceOrParameters<TViewModel> On(params string[] inputEvent);
}
