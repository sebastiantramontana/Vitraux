namespace Vitraux.Modeling.Building.Selectors.Insertion;

internal record class FetchInsertionSelector(Uri Uri)
    : InsertionSelector(InsertionSelection.FromFetch, Uri.ToString());
