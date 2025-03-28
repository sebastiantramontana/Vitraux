namespace Vitraux.Modeling.Data.Selectors.Values.Insertions;

internal record class InsertElementUriSelectorUri(ElementSelectorBase ElementToAppend, Uri Uri) 
    : InsertElementUriSelectorBase(ElementToAppend);
