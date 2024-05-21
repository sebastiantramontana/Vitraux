namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IPopulationToNextElementSelector<TNext> : IElementSelectorBuilder
{
    TNext FromTemplate(string templateid);
    TNext FromFetch(Uri uri);
}