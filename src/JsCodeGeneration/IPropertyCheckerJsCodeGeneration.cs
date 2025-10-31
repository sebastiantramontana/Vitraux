using System.Text;

namespace Vitraux.JsCodeGeneration;

internal interface IPropertyCheckerJsCodeGeneration
{
    StringBuilder GenerateBeginCheckJs(StringBuilder jsBuilder, string objectParentName, string valueObjectName, int indentCount);
    StringBuilder GenerateEndCheckJs(StringBuilder jsBuilder, int indentCount);
}
