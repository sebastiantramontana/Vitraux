using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsAlwaysJsCodeGenerator(
    IQueryElementsDeclaringAlwaysCodeGenerator generator,
    IQueryElementsJsGenerator builder)
    : IQueryElementsAlwaysJsCodeGenerator
{
    public StringBuilder GenerateJsCode(StringBuilder jsBuilder, IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName, int indentCount)
        => builder.GenerateJsCode(jsBuilder, generator, jsObjectNames, parentElementObjectName, indentCount);
}