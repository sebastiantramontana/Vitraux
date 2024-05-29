namespace Vitraux.Modeling.Building.Selectors.Insertion;

internal abstract record class FetchInsertionSelectorBase()
    : InsertionSelectorBase(InsertionSelection.FromFetch);
