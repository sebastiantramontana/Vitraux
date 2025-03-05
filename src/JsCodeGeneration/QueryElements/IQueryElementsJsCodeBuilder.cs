using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeBuilder
{
    string BuildJsCode(IQueryElementsDeclaringJsCodeGenerator declaringJsCodeGenerator, IEnumerable<ElementObjectName> selectors, string parentElementObjectName);
}


