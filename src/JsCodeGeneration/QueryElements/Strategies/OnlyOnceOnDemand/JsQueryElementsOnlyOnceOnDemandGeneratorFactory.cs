using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class JsQueryElementsOnlyOnceOnDemandGeneratorFactory(
    IQueryElementsDeclaringOnlyOnceOnDemandByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator jsQueryElementsByTemplateGenerator) : IJsQueryElementsOnlyOnceOnDemandGeneratorFactory
{
    public IQueryElementsDeclaringJsCodeGenerator GetInstance(ElementSelectorBase elementSelector)
        => elementSelector.SelectionBy switch
        {
            ElementSelection.Id => jsQueryElementsByIdGenerator,
            ElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
            ElementSelection.Template => jsQueryElementsByTemplateGenerator,
            _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for ElementSelector: {elementSelector}")
        };
}
