using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator : IQueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
    {
        var elementCodeBuilder = new StringBuilder();
        elementCodeBuilder.Append($"const {elementObjectName.Name} = globalThis.vitraux.storedElements.elements.{elementObjectName.Name};");

        if (elementObjectName is PopulatingElementObjectName populatingObjectName)
        {
            elementCodeBuilder.AppendLine();
            elementCodeBuilder.Append($"const {populatingObjectName.AppendToName} = globalThis.vitraux.storedElements.elements.{populatingObjectName.AppendToName};");
        }

        return elementCodeBuilder.ToString();
    }
}
