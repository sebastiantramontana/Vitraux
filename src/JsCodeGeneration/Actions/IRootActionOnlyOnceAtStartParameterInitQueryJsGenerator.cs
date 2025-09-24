using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.Actions;

internal interface IRootActionOnlyOnceAtStartParameterInitQueryJsGenerator
{
    StringBuilder GenerateJs(StringBuilder stringBuilder, IEnumerable<JsElementObjectName> jsParametersObjectNames);
}
