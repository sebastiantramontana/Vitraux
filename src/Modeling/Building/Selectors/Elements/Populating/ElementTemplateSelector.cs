namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

internal record class ElementTemplateSelector : PopulatingElementSelector
{
    internal ElementTemplateSelector(string templateId)
        : base(ElementSelection.Template, templateId)
    {
    }
}
