using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class JsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorContext(
    IQueryElementsDeclaringOnlyOnceOnDemandByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator)
    : IJsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorContext
{
    public IQueryElementsDeclaringJsCodeGenerator GetStrategy(PopulatingAppendToElementSelection selectionBy)
     => selectionBy switch
     {
         PopulatingAppendToElementSelection.Id => jsQueryElementsByIdGenerator,
         PopulatingAppendToElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
         _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for FromTemplateElementSelection: {selectionBy}")
     };
}
