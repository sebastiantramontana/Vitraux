namespace Vitraux.Modeling.Building.Selectors.Insertion;

internal abstract record class TemplateInsertionSelectorBase()
    : InsertionSelectorBase(InsertionSelection.FromTemplate);
