using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator(IJsQueryElementsOnlyOnceOnDemandGeneratorContext context)
    : IQueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, JsElementObjectName jsElementObjectName)
        => context
            .GetStrategy(jsElementObjectName.AssociatedSelector)
            .GenerateJsCode(parentObjectName, jsElementObjectName);
}
