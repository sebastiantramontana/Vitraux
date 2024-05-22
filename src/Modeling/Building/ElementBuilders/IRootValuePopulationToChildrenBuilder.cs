namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IRootValuePopulationToChildrenBuilder<TNext>
{
    IRootValuePopulationChildrenElementSelector<TNext> ToChildren { get; }
}