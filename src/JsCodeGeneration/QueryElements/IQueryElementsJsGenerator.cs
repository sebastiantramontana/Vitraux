using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsGenerator
{
    string GenerateJsCode(IQueryElementsDeclaringJsGenerator declaringJsCodeGenerator, IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName);
}

