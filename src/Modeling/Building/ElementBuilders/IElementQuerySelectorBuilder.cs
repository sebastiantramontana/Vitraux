namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IElementQuerySelectorBuilder<TNext> : IElementSelectorBuilder
{
    TNext ByQuery(string query);
}