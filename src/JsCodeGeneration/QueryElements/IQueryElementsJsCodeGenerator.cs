using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeGenerator
{
    string GenerateJsCode(IEnumerable<JsObjectName> jsObjectNames, string parentElementObjectName);
}
