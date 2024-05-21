namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

internal record class ElementFetchSelector : PopulatingElementSelector
{
    internal ElementFetchSelector(Uri uri)
        : base(ElementSelection.Fetch, uri.ToString())
    {
    }
}
