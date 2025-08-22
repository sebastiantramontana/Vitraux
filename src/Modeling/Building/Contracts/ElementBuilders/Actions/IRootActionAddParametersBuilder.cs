namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionAddParametersBuilder<TViewModel> : IRootActionSourceFinallizableBuilder<TViewModel>
{
    IRootActionPassValueOrNameSourceBuilder<TViewModel> AddParameters { get; }
}
