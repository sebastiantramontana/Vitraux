using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal interface IJsQueryElementsOnlyOnceOnDemandGeneratorFactory
{
    IQueryElementsDeclaringJsCodeGenerator GetInstance(ElementSelector elementSelector);
}
