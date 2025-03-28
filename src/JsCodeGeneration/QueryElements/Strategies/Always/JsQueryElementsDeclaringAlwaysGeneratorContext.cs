using Vitraux.Modeling.Data.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class JsQueryElementsDeclaringAlwaysGeneratorContext(
    IQueryElementsDeclaringAlwaysByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator,
    IQueryElementsDeclaringAlwaysByTemplateJsCodeGenerator jsQueryElementsByTemplateGenerator,
    IQueryElementsDeclaringAlwaysByFetchJsCodeGenerator jsQueryElementsByFetchGenerator)
    : IJsQueryElementsDeclaringAlwaysGeneratorContext
{
    public IQueryElementsDeclaringJsCodeGenerator GetStrategy(ElementSelectorBase elementSelector)
        => elementSelector switch
        {
            ElementIdSelectorBase => jsQueryElementsByIdGenerator,
            ElementQuerySelectorBase => jsQueryElementsByQuerySelectorGenerator,
            ElementTemplateSelectorBase => jsQueryElementsByTemplateGenerator,
            ElementSelection.Uri => jsQueryElementsByFetchGenerator,
            _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for ElementSelector: {elementSelector}")
        };
}
