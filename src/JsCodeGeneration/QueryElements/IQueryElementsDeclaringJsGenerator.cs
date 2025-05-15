using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsDeclaringJsGenerator
{
    string GenerateJsCode(string parentElementObjectName, JsObjectName jsObjectName);
}