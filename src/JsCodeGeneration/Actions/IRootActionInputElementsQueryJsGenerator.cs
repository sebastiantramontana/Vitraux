using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.Actions;

internal interface IRootActionInputElementsQueryJsGenerator
{
    StringBuilder GenerateJs(StringBuilder stringBuilder, IEnumerable<JsElementObjectName> jsInputObjectNames);
}
