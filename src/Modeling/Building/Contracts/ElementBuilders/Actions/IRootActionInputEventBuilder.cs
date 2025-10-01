namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionInputEventBuilder<TViewModel>
{
    IRootActionSourceFinallizableBuilder<TViewModel> On(params string[] inputEvent);
}
