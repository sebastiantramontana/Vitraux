using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeGenerator
{
    string GenerateJsCode(IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName);
}
