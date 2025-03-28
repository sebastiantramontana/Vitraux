namespace Vitraux.Modeling.Data.Selectors.Elements.Populating;

internal abstract record class PopulatingAppendToElementQuerySelectorBase()
    : PopulatingAppendToElementSelectorBase(PopulatingAppendToElementSelection.QuerySelector);
