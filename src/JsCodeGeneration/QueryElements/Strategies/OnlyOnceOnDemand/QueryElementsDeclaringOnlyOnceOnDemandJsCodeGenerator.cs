using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator(IJsQueryElementsOnlyOnceOnDemandGeneratorFactory factory) : IQueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => factory
            .GetInstance(elementObjectName.AssociatedSelector)
            .GenerateJsCode(parentObjectName, elementObjectName);
}
