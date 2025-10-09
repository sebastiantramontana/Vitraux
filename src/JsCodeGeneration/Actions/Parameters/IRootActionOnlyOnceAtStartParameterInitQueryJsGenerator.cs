using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal interface IRootActionOnlyOnceAtStartParameterInitQueryJsGenerator
{
    StringBuilder GenerateJs(StringBuilder stringBuilder, IEnumerable<JsElementObjectName> jsParametersObjectNames);
}
