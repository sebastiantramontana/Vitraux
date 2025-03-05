using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsDeclaringJsCodeGenerator
{
    string GenerateJsCode(string parentElementObjectName, ElementObjectName elementObjectName);
}


