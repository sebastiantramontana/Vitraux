using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal interface IJsQueryElementsDeclaringAlwaysGeneratorContext
{
    IQueryElementsDeclaringJsCodeGenerator GetStrategy(ElementSelectorBase elementSelector);
}
