using Vitraux.Modeling.Data.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal interface IJsQueryElementsOnlyOnceOnDemandGeneratorContext
{
    IQueryElementsDeclaringJsCodeGenerator GetStrategy(ElementSelectorBase elementSelector);
}
