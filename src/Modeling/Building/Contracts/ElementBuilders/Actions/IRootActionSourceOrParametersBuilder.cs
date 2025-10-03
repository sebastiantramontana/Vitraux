namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionSourceOrParametersBuilder<TViewModel> : IRootParametrizableActionSourceBuilder<TViewModel>, IRootActionPassValueOrNameSourceBuilder<TViewModel>
{ }
