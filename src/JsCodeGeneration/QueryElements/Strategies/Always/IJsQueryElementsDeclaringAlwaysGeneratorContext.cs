using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal interface IJsQueryElementsDeclaringAlwaysGeneratorContext
{
    IQueryElementsDeclaringJsGenerator GetStrategy(SelectorBase selector);
}
