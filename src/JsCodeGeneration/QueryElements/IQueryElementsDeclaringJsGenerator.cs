using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsDeclaringJsGenerator
{
    string GenerateJsCode(string parentElementObjectName, JsElementObjectName jsElementObjectName);
}