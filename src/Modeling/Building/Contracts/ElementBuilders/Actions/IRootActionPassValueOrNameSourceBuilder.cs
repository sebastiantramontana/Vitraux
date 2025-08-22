namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionPassValueOrNameSourceBuilder<TViewModel> : IRootActionParameterNameBuilder<TViewModel>
{
    IRootActionParameterNameFinallizableBuilder<TViewModel> PassValue { get; }
}
