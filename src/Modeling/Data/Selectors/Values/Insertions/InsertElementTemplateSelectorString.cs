namespace Vitraux.Modeling.Data.Selectors.Values.Insertions;

internal record class InsertElementTemplateSelectorString(ElementSelectorBase ElementToAppend,  string TemplateId)
    : InsertElementTemplateSelectorBase(ElementToAppend);
