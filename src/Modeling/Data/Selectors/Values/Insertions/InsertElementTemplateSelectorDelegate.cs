namespace Vitraux.Modeling.Data.Selectors.Values.Insertions;

internal record class InsertElementTemplateSelectorDelegate(ElementSelectorBase ElementToAppend, Delegate TemplateDelegate)
    : InsertElementTemplateSelectorBase(ElementToAppend);