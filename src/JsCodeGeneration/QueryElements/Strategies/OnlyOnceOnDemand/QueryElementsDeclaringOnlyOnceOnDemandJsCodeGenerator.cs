using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator(IJsQueryElementsOnlyOnceOnDemandGeneratorContext context) : IQueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => context
            .GetStrategy(elementObjectName.AssociatedSelector)
            .GenerateJsCode(parentObjectName, elementObjectName);
}
