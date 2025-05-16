using Vitraux.Modeling.Data.Selectors;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class JsQueryElementsOnlyOnceOnDemandGeneratorContext(
    IQueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsGenerator jsQueryElementsByQuerySelectorGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandValueByTemplateJsGenerator jsQueryElementsValueByTemplateGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandValueByUriJsGenerator jsQueryElementsValueByUriGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandCollectionByTemplateJsGenerator jsQueryElementsCollectionByTemplateGenerator,
    IQueryElementsDeclaringOnlyOnceOnDemandCollectionByUriJsGenerator jsQueryElementsCollectionByUriGenerator,
    INotImplementedSelector notImplementedSelector)
    : IJsQueryElementsOnlyOnceOnDemandGeneratorContext
{
    public IQueryElementsDeclaringJsGenerator GetStrategy(SelectorBase selector)
        => selector switch
        {
            ElementIdSelectorBase => jsQueryElementsByIdGenerator,
            ElementQuerySelectorBase => jsQueryElementsByQuerySelectorGenerator,
            InsertElementTemplateSelectorBase => jsQueryElementsValueByTemplateGenerator,
            InsertElementUriSelectorBase => jsQueryElementsValueByUriGenerator,
            TemplateInsertionSelectorBase => jsQueryElementsCollectionByTemplateGenerator,
            UriInsertionSelectorBase => jsQueryElementsCollectionByUriGenerator,
            _ => notImplementedSelector.ThrowNotImplementedException<IQueryElementsDeclaringJsGenerator>(selector)
        };
}
