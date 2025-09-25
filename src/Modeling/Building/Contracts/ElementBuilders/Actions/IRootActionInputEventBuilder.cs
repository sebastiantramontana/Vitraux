namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionInputEventBuilder<TViewModel>
{
    IRootActionPassValueOrNameSourceBuilder<TViewModel> On(string inputEvent);
}
