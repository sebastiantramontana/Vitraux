using System.Text;

namespace Vitraux.JsCodeGeneration;

internal interface IPromiseJsGenerator
{
    string ReturnResolvedPromiseJsLine { get; }
    StringBuilder GenerateJs(StringBuilder jsBuilder, int indentCount);
}
