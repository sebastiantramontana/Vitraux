namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionSourceOrParameters<TViewModel> : IRootParametrizableActionSourceBuilder<TViewModel>, IRootActionPassValueOrNameSourceBuilder<TViewModel>
{ }