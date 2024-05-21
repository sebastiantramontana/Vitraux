namespace Vitraux.Modeling.Building.Selectors.TableRows;

internal record class FetchRowSelector(Uri Uri)
    : RowSelector(RowSelection.FromFetch, Uri.ToString());
