using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsOnlyOnceOnDemandJsCodeGenerator(
    IQueryElementsJsGenerator generator,
    IQueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator onlyOnceOnDemandGenerator)
    : IQueryElementsOnlyOnceOnDemandJsCodeGenerator
{
    public StringBuilder GenerateJsCode(StringBuilder jsBuilder, IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName, int indentCount)
        => generator.GenerateJsCode(jsBuilder, onlyOnceOnDemandGenerator, jsObjectNames, parentElementObjectName, indentCount);
}