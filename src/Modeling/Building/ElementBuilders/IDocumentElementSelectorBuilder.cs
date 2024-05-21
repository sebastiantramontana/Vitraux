namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IDocumentElementSelectorBuilder<TNext> : IElementQuerySelectorBuilder<TNext>
{
    TNext ById(string id);
}