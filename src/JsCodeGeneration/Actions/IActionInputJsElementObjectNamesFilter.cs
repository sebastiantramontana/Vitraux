using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions;

internal interface IActionInputJsElementObjectNamesFilter
{
    IEnumerable<JsElementObjectName> Filter(ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames);
}
