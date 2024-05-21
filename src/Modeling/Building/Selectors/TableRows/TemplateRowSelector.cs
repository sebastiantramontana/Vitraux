namespace Vitraux.Modeling.Building.Selectors.TableRows;

internal record class TemplateRowSelector(string TemplateId)
    : RowSelector(RowSelection.FromTemplate, TemplateId);
