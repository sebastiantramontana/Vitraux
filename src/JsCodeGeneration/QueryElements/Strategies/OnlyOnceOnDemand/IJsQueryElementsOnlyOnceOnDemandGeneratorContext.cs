using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal interface IJsQueryElementsOnlyOnceOnDemandGeneratorContext
{
    IQueryElementsDeclaringJsCodeGenerator GetStrategy(ElementSelectorBase elementSelector);
}
