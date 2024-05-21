using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class JsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory(
    IQueryElementsDeclaringAlwaysByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator)
    : IJsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory
{
    public IQueryElementsDeclaringJsCodeGenerator GetInstance(PopulatingAppendToElementSelection selectionBy)
        => selectionBy switch
        {
            PopulatingAppendToElementSelection.Id => jsQueryElementsByIdGenerator,
            PopulatingAppendToElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
            _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for FromTemplateElementSelection: {selectionBy}")
        };
}
