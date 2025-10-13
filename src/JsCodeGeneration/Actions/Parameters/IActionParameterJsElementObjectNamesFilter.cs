using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal interface IActionParameterJsElementObjectNamesFilter
{
    IEnumerable<JsElementObjectName> Filter(ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames);
}
