using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal interface IRootSingleParametrizableActionJsGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, string vmKey, ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames, QueryElementStrategy queryElementStrategy);
}
