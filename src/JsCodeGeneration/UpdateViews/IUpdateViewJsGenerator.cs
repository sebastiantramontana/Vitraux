using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.UpdateViews;

internal interface IUpdateViewJsGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, QueryElementStrategy queryElementStrategy, FullObjectNames fullObjectNames, string parentObjectName, string parentElementObjectName, int indentCount);
}
