using System.Text;
using Vitraux.JsCodeGeneration.Actions.Parameters;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootSingleActionJsGenerator(
    IRootSingleParameterlessActionJsGenerator parameterlessActionJsGenerator,
    IRootSingleParametrizableActionJsGenerator parametrizableActionJsGenerator) : IRootSingleActionJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, string vmKey, ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames, QueryElementStrategy queryElementStrategy)
        => HasActionParameters(action)
            ? parametrizableActionJsGenerator.GenerateJs(jsBuilder, vmKey, action, jsAllElementObjectNames, queryElementStrategy)
            : parameterlessActionJsGenerator.GenerateJs(jsBuilder, vmKey, action, jsAllElementObjectNames);

    private static bool HasActionParameters(ActionData action)
        => action.Parameters.Any() || action.PassInputValueParameter;
}
