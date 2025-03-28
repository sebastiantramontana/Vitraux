namespace Vitraux.Modeling.Data.Selectors.Values.Insertions;

internal record class InsertElementUriSelectorDelegate(ElementSelectorBase ElementToAppend, Delegate UriDelegate) 
    : InsertElementUriSelectorBase(ElementToAppend);