using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class JsQueryPopulatingElementsDeclaringAlwaysGeneratorContext(
    IQueryElementsDeclaringAlwaysByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator)
    : IJsQueryPopulatingElementsDeclaringAlwaysGeneratorContext
{
    public IQueryElementsDeclaringJsCodeGenerator GetStrategy(PopulatingAppendToElementSelection selectionBy)
        => selectionBy switch
        {
            PopulatingAppendToElementSelection.Id => jsQueryElementsByIdGenerator,
            PopulatingAppendToElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
            _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for PopulatingAppendToElementSelection: {selectionBy}")
        };
}
