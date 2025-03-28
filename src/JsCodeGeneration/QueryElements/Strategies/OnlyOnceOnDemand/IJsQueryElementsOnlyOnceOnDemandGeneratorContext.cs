using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal interface IJsQueryElementsOnlyOnceOnDemandGeneratorContext
{
    IQueryElementsDeclaringJsCodeGenerator GetStrategy(ElementSelectorBase elementSelector);
}
