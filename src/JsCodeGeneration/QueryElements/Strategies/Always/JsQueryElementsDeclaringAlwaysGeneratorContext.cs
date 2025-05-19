using Vitraux.Helpers;
using Vitraux.Modeling.Data.Selectors;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class JsQueryElementsDeclaringAlwaysGeneratorContext(
    IQueryElementsDeclaringAlwaysByIdJsGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringAlwaysByQuerySelectorJsGenerator jsQueryElementsByQuerySelectorGenerator,
    IQueryElementsDeclaringAlwaysValueByTemplateJsGenerator jsQueryElementsValueByTemplateGenerator,
    IQueryElementsDeclaringAlwaysValueByUriJsGenerator jsQueryElementsValueByUrihGenerator,
    IQueryElementsDeclaringAlwaysCollectionByTemplateJsGenerator jsQueryElementsCollectionByTemplateGenerator,
    IQueryElementsDeclaringAlwaysCollectionByUriJsGenerator jsQueryElementsCollectionByUriGenerator,
    INotImplementedCaseGuard notImplementedSelector)
    : IJsQueryElementsDeclaringAlwaysGeneratorContext
{
    public IQueryElementsDeclaringJsGenerator GetStrategy(SelectorBase selector)
        => selector switch
        {
            ElementIdSelectorBase => jsQueryElementsByIdGenerator,
            ElementQuerySelectorBase => jsQueryElementsByQuerySelectorGenerator,
            InsertElementTemplateSelectorBase => jsQueryElementsValueByTemplateGenerator,
            InsertElementUriSelectorBase => jsQueryElementsValueByUrihGenerator,
            TemplateInsertionSelectorBase => jsQueryElementsCollectionByTemplateGenerator,
            UriInsertionSelectorBase => jsQueryElementsCollectionByUriGenerator,
            _ => notImplementedSelector.ThrowException<IQueryElementsDeclaringJsGenerator>(selector)
        };
}
