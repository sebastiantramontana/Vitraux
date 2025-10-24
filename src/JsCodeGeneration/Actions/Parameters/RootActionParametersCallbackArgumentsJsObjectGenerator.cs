using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionParametersCallbackArgumentsJsObjectGenerator(
    ICodeFormatter codeFormatter,
    IActionParameterGettingValueCallGenerator actionParameterGettingValueCallGenerator) : IRootActionParametersCallbackArgumentsJsObjectGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, ActionData action, IEnumerable<JsElementObjectName> jsParameterObjectNames, int indentCount)
        => jsParameterObjectNames.Any()
            ? jsParameterObjectNames
                .Aggregate(jsBuilder, (sb, p) => sb.AddLine(GenerateJsObjectPropertyValue, p.Name, GetActionParameter(p.AssociatedSelector, action), indentCount))
                .TrimEnd()
                .RemoveLastCharacter(',')
            : jsBuilder;

    private static ActionParameter GetActionParameter(SelectorBase selector, ActionData action)
        => action.Parameters.Single(p => p.Selector == selector);

    private StringBuilder GenerateJsObjectPropertyValue(StringBuilder jsBuilder, string jsParameterObjectName, ActionParameter parameter, int indentCount)
        => jsBuilder.Append(codeFormatter.Indent(GenerateJsObjectPropertyValue(jsParameterObjectName, parameter), indentCount));

    private string GenerateJsObjectPropertyValue(string jsParameterObjectName, ActionParameter parameter)
        => $"{GenerateObjectProperty(parameter.ParamName)}: {GenerateObjectValueFromPlace(jsParameterObjectName, parameter.ElementPlace)},";

    private static string GenerateObjectProperty(string propertyName)
        => $"'{propertyName}'";

    private string GenerateObjectValueFromPlace(string jsParameterObjectName, ElementPlace elementPlace)
        => actionParameterGettingValueCallGenerator.GenerateJs(jsParameterObjectName, elementPlace);
}
