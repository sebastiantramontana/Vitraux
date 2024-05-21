namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IPopulatingChildrenElementSelector<TNext>
{
    TNext ByQuery(string query);
}