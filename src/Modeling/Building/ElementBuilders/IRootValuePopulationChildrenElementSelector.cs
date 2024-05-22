namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IRootValuePopulationChildrenElementSelector<TNext>
{
    TNext ByQuery(string query);
}