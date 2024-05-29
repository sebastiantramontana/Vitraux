namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IDocumentElementSelectorBuilder<TNext, TViewModel> : IElementQuerySelectorBuilder<TNext, TViewModel>
{
    TNext ById(string id);
    TNext ById(Func<TViewModel, string> idFunc);
}