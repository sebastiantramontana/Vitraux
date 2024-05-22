namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IToElementsBuilder<TViewModel, TNextElementSelectorBuilder>
    where TNextElementSelectorBuilder : IElementSelectorBuilder
{
    TNextElementSelectorBuilder ToElements { get; }
}
