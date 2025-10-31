using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryElementsJsGenerator(ICodeFormatter codeFormatter) : IQueryElementsJsGenerator
{
    public StringBuilder GenerateJsCode(StringBuilder jsBuilder, IQueryElementsDeclaringJsGenerator declaringJsCodeGenerator, IEnumerable<JsElementObjectName> jsObjectNames, string parentObjectName, int indentCount)
        => jsObjectNames.Any()
            ? jsObjectNames
                .Aggregate(jsBuilder, (sb, jsElementObjectName) => sb.AddLine(BuildElementDeclarationCode, declaringJsCodeGenerator, jsElementObjectName, parentObjectName, indentCount))
                .TrimEnd()
            : jsBuilder;

    private StringBuilder BuildElementDeclarationCode(StringBuilder jsBuilder, IQueryElementsDeclaringJsGenerator declaringJsCodeGenerator, JsElementObjectName jsElementObjectName, string parentObjectName, int indentCount)
        => jsBuilder.Append(codeFormatter.IndentLine(declaringJsCodeGenerator.GenerateJsCode(parentObjectName, jsElementObjectName), indentCount));
}