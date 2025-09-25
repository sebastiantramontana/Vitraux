namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionPassValueOrNameSourceBuilder<TViewModel> : IRootActionAddParameterNameBuilder<TViewModel>, IRootActionSourceFinallizableBuilder<TViewModel>
{
    IRootActionAddParameterNameFinallizableBuilder<TViewModel> AddParameterValue { get; }
}
