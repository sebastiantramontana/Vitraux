namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

internal abstract record class ElementFetchSelectorBase()
    : PopulatingElementSelectorBase(ElementSelection.Fetch);
