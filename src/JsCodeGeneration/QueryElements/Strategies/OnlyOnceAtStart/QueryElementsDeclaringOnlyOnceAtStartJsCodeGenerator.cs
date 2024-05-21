using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator : IQueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
    {
        var elementCodeBuilder = new StringBuilder();
        elementCodeBuilder.Append($"const {elementObjectName.Name} = globalThis.vitraux.storedElements.elements.{parentObjectName}.{elementObjectName.Name};");

        if (elementObjectName is ElementTemplateObjectName templateObjectName)
        {
            elementCodeBuilder.AppendLine();
            elementCodeBuilder.Append($"const {templateObjectName.AppendToName} = globalThis.vitraux.storedElements.elements.{parentObjectName}.{templateObjectName.AppendToName};");
        }

        return elementCodeBuilder.ToString();
    }
}
