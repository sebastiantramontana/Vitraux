using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator : IQueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
    {
        var elementCodeBuilder = new StringBuilder();
        elementCodeBuilder.Append($"const {elementObjectName.JsObjName} = globalThis.vitraux.storedElements.elements.{elementObjectName.JsObjName};");

        if (elementObjectName is InsertElementObjectName populatingObjectName)
        {
            elementCodeBuilder.AppendLine();
            elementCodeBuilder.Append($"const {populatingObjectName.AppendToJsObjNameName} = globalThis.vitraux.storedElements.elements.{populatingObjectName.AppendToJsObjNameName};");
        }

        return elementCodeBuilder.ToString();
    }
}
