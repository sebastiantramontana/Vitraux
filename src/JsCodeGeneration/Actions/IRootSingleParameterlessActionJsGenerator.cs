using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions;

internal interface IRootSingleParameterlessActionJsGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, string vmKey, ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames);
}
