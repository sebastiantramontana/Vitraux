using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootSingleParameterlessActionJsGenerator(
    IRootActionInputElementsQueryJsGenerator rootActionInputElementsQueryJsGenerator,
    IActionInputJsElementObjectNamesFilter actionInputJsElementObjectNames,
    IRootActionInputEventsRegistrationJsGenerator rootActionInputEventsRegistrationJsGenerator)
    : IRootSingleParameterlessActionJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, string vmKey, ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames)
    {
        var jsInputObjectNames = actionInputJsElementObjectNames.Filter(action, jsAllElementObjectNames);

        return jsBuilder
            .AddTwoLines(rootActionInputElementsQueryJsGenerator.GenerateJs, jsInputObjectNames)
            .Add(rootActionInputEventsRegistrationJsGenerator.GenerateJs, action, jsInputObjectNames, vmKey);
    }
}
