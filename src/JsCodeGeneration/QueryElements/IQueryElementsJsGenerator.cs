using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsGenerator
{
    string GenerateJsCode(IQueryElementsDeclaringJsGenerator declaringJsCodeGenerator, IEnumerable<JsObjectName> jsObjectNames, string parentElementObjectName);
}

