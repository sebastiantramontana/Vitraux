using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator(IJsQueryElementsOnlyOnceOnDemandGeneratorContext context)
    : IQueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => context
            .GetStrategy(jsObjectName.AssociatedSelector)
            .GenerateJsCode(parentObjectName, jsObjectName);
}
