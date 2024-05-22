namespace Vitraux.Modeling.Building.Selectors.Insertion;

internal record class TemplateInsertionSelector(string TemplateId)
    : InsertionSelector(InsertionSelection.FromTemplate, TemplateId);
