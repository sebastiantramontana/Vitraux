using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal interface IJsQueryElementsDeclaringAlwaysGeneratorContext
{
    IQueryElementsDeclaringJsCodeGenerator GetStrategy(ElementSelectorBase elementSelector);
}
