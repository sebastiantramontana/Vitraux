using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryElementsJsGenerator : IQueryElementsJsGenerator
{
    public string GenerateJsCode(IQueryElementsDeclaringJsGenerator declaringJsCodeGenerator, IEnumerable<JsObjectName> jsObjectNames, string parentObjectName)
        => jsObjectNames
            .Aggregate(new StringBuilder(), (sb, jsObjectName) => sb.AppendLine(BuildElementDeclarationCode(declaringJsCodeGenerator, jsObjectName, parentObjectName)))
            .ToString()
            .TrimEnd();

    private static string BuildElementDeclarationCode(IQueryElementsDeclaringJsGenerator declaringJsCodeGenerator, JsObjectName jsObjectName, string parentObjectName)
        => declaringJsCodeGenerator.GenerateJsCode(parentObjectName, jsObjectName);
}


