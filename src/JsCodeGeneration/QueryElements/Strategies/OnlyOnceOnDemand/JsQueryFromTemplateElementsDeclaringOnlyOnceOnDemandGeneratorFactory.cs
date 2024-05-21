using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class JsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorFactory(
    IQueryElementsDeclaringOnlyOnceOnDemandByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator)
    : IJsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorFactory
{
    public IQueryElementsDeclaringJsCodeGenerator GetInstance(PopulatingAppendToElementSelection selectionBy)
     => selectionBy switch
     {
         PopulatingAppendToElementSelection.Id => jsQueryElementsByIdGenerator,
         PopulatingAppendToElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
         _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for FromTemplateElementSelection: {selectionBy}")
     };
}
