namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IElementQuerySelectorBuilder<TNext, TViewModel> : IElementSelectorBuilder
{
    TNext ByQuery(string query);
    TNext ByQuery(Func<TViewModel, string> queryFunc);
}