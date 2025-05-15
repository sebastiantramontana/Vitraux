using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsOnlyOnceOnDemandJsCodeGenerator(
    IQueryElementsJsGenerator generator,
    IQueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator onlyOnceOnDemandGenerator)
    : IQueryElementsOnlyOnceOnDemandJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<JsObjectName> jsObjectNames, string parentElementObjectName)
        => generator.GenerateJsCode(onlyOnceOnDemandGenerator, jsObjectNames, parentElementObjectName);
}