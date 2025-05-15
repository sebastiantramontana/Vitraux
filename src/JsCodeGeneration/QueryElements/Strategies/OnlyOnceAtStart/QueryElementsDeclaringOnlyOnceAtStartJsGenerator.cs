using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsDeclaringOnlyOnceAtStartJsGenerator : IQueryElementsDeclaringOnlyOnceAtStartJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => $"const {jsObjectName.Name} = globalThis.vitraux.storedElements.elements.{jsObjectName.Name};";
}