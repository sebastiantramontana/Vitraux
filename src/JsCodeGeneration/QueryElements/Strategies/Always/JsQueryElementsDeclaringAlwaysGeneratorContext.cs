using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class JsQueryElementsDeclaringAlwaysGeneratorContext(
    IQueryElementsDeclaringAlwaysByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator,
    IQueryElementsDeclaringAlwaysByTemplateJsCodeGenerator jsQueryElementsByTemplateGenerator)
    : IJsQueryElementsDeclaringAlwaysGeneratorContext
{
    public IQueryElementsDeclaringJsCodeGenerator GetStrategy(ElementSelectorBase elementSelector)
        => elementSelector.SelectionBy switch
        {
            ElementSelection.Id => jsQueryElementsByIdGenerator,
            ElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
            ElementSelection.Template => jsQueryElementsByTemplateGenerator,
            _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for ElementSelector: {elementSelector}")
        };
}
