using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsDeclaringOnlyOnceAtStartJsGenerator : IQueryElementsDeclaringOnlyOnceAtStartJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsElementObjectName jsElementObjectName)
        => $"const {jsElementObjectName.Name} = globalThis.vitraux.storedElements.elements.{jsElementObjectName.Name};";
}