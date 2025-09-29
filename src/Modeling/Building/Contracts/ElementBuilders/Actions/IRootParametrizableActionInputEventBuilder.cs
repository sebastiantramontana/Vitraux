namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootParametrizableActionInputEventBuilder<TViewModel>
{
    IRootActionPassValueOrNameSourceBuilder<TViewModel> On(string inputEvent);
}
