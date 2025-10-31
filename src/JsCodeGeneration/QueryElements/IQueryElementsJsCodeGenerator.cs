using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeGenerator
{
    StringBuilder GenerateJsCode(StringBuilder jsBuilder, IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName, int indentCount);
}
