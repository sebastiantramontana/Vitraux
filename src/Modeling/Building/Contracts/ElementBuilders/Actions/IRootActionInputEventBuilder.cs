namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionInputEventBuilder<TViewModel>
{
    IRootActionAddParametersBuilder<TViewModel> On(string inputEvent);
}
