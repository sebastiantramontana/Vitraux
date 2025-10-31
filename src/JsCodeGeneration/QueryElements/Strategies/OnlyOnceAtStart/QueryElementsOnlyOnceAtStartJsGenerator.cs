using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsOnlyOnceAtStartJsGenerator(
    IQueryElementsJsGenerator builder,
    IQueryElementsDeclaringOnlyOnceAtStartJsGenerator declaringGenerator)
    : IQueryElementsOnlyOnceAtStartJsGenerator
{
    public StringBuilder GenerateJsCode(StringBuilder jsBuilder, IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName, int indentCount)
        => builder.GenerateJsCode(jsBuilder, declaringGenerator, jsObjectNames, parentElementObjectName, indentCount);
}