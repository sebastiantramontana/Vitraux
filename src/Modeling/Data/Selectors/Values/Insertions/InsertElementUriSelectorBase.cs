namespace Vitraux.Modeling.Data.Selectors.Values.Insertions;

internal abstract record class InsertElementUriSelectorBase(ElementSelectorBase ElementToAppend) 
    : InsertElementSelectorBase(ElementToAppend);
