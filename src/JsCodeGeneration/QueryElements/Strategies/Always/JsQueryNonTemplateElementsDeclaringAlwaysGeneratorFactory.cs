using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class JsQueryFromTemplateElementsDeclaringAlwaysGeneratorContext(
    IQueryElementsDeclaringAlwaysByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator)
    : IJsQueryFromTemplateElementsDeclaringAlwaysGeneratorContext
{
    public IQueryElementsDeclaringJsCodeGenerator GetStrategy(PopulatingAppendToElementSelection selectionBy)
        => selectionBy switch
        {
            PopulatingAppendToElementSelection.Id => jsQueryElementsByIdGenerator,
            PopulatingAppendToElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
            _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for FromTemplateElementSelection: {selectionBy}")
        };
}
