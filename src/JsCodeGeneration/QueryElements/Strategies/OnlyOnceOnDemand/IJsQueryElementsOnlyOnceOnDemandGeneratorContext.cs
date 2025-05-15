using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal interface IJsQueryElementsOnlyOnceOnDemandGeneratorContext
{
    IQueryElementsDeclaringJsGenerator GetStrategy(SelectorBase selector);
}
