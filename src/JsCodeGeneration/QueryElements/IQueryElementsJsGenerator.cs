using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsGenerator
{
    StringBuilder GenerateJsCode(StringBuilder jsBuilder, IQueryElementsDeclaringJsGenerator declaringJsCodeGenerator, IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName, int indentCount);
}

