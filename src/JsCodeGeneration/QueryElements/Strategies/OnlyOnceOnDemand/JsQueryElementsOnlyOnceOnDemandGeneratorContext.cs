using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class JsQueryElementsOnlyOnceOnDemandGeneratorContext(
    IQueryElementsDeclaringOnlyOnceOnDemandByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator jsQueryElementsByTemplateGenerator) : IJsQueryElementsOnlyOnceOnDemandGeneratorContext
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
