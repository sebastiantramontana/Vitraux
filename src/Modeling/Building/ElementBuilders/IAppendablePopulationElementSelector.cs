namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IAppendablePopulationElementSelector<TNext> : IPopulationToNextElementSelector<IToChildrenElementSelector<TNext>>
{
}