using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsOnlyOnceOnDemandJsCodeGenerator(
    IQueryElementsJsGenerator generator,
    IQueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator onlyOnceOnDemandGenerator)
    : IQueryElementsOnlyOnceOnDemandJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName)
        => generator.GenerateJsCode(onlyOnceOnDemandGenerator, jsObjectNames, parentElementObjectName);
}