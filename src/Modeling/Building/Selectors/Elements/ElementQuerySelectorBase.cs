namespace Vitraux.Modeling.Building.Selectors.Elements;

internal abstract record class ElementQuerySelectorBase()
    : ElementSelectorBase(ElementSelection.QuerySelector);
