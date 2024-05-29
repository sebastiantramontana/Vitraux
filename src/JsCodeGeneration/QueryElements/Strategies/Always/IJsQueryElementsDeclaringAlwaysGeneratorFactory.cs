using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal interface IJsQueryElementsDeclaringAlwaysGeneratorFactory
{
    IQueryElementsDeclaringJsCodeGenerator GetInstance(ElementSelectorBase elementSelector);
}
