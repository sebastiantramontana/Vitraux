using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryElementsJsGenerator : IQueryElementsJsGenerator
{
    public string GenerateJsCode(IQueryElementsDeclaringJsGenerator declaringJsCodeGenerator, IEnumerable<JsElementObjectName> jsObjectNames, string parentObjectName)
        => jsObjectNames
            .Aggregate(new StringBuilder(), (sb, jsElementObjectName) => sb.AppendLine(BuildElementDeclarationCode(declaringJsCodeGenerator, jsElementObjectName, parentObjectName)))
            .ToString()
            .TrimEnd();

    private static string BuildElementDeclarationCode(IQueryElementsDeclaringJsGenerator declaringJsCodeGenerator, JsElementObjectName jsElementObjectName, string parentObjectName)
        => declaringJsCodeGenerator.GenerateJsCode(parentObjectName, jsElementObjectName);
}


