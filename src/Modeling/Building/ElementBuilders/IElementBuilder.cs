namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IElementBuilder<TViewModel, TNextElementSelectorBuilder>
    where TNextElementSelectorBuilder : IElementSelectorBuilder
{
    TNextElementSelectorBuilder ToElements { get; }
}
