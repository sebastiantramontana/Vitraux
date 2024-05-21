using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryElementsJsCodeBuilder : IQueryElementsJsCodeBuilder
{
    public string BuildJsCode(IQueryElementsDeclaringJsCodeGenerator declaringJsCodeGenerator, IEnumerable<ElementObjectName> generatedElements, string parentObjectName)
        => generatedElements
            .Aggregate(new StringBuilder(), (sb, element) => sb.AppendLine(BuildElementDeclarationCode(declaringJsCodeGenerator, element, parentObjectName)))
            .ToString();

    private static string BuildElementDeclarationCode(IQueryElementsDeclaringJsCodeGenerator declaringJsCodeGenerator, ElementObjectName element, string parentObjectName)
        => declaringJsCodeGenerator.GenerateJsCode(parentObjectName, element);
}


