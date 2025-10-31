using System.Text;
using Vitraux.Execution.ViewModelNames;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.CustomJsGeneration;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.Values.JsTargets;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementsValueJsGenerator(
    ITargetElementsDirectUpdateValueJsGenerator directUpdateValueJsGenerator,
    ITargetElementsUpdateValueInsertJsGenerator updateValueInsertJsGenerator,
    IViewModelKeyGenerator viewModelKeyGenerator,
    IExecuteUpdateViewFunctionCall executeUpdateViewFunctionCall,
    ICustomJsJsGenerator customJsJsGenerator,
    ICodeFormatter codeFormatter,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : ITargetElementsValueJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, string parentValueObjectName, ValueObjectNameWithJsTargets valueObjectNameWithTargets, int indentCount)
        => valueObjectNameWithTargets.JsTargets.Any()
            ? valueObjectNameWithTargets.JsTargets
                .Aggregate(jsBuilder, (sb, target)
                    => sb.AddLine(GenerateJsCodeByJsTarget, target, parentValueObjectName, valueObjectNameWithTargets.Name, indentCount))
                .TrimEnd()
            : jsBuilder;

    private StringBuilder GenerateJsCodeByJsTarget(StringBuilder jsBuilder, ValueJsTarget target, string parentValueObjectName, string valueObjName, int indentCount)
        => target switch
        {
            ValueElementTargetJsObjectName valueJsElementTarget => jsBuilder.Add(GenerateJsByElementValueTarget, valueJsElementTarget, parentValueObjectName, valueObjName, indentCount),
            ValueCustomJsTarget customJsTarget => jsBuilder.Add(GenerateJsByCustomJsValueTarget, customJsTarget.AssociatedCustomJsTarget, parentValueObjectName, valueObjName, indentCount),
            ValueOwnMappingTarget valueOwnMappingTarget => jsBuilder.Add(GenerateJsByOwnMappingTarget, valueOwnMappingTarget, parentValueObjectName, valueObjName, indentCount),
            _ => notImplementedCaseGuard.ThrowException<StringBuilder>(target)
        };

    private StringBuilder GenerateJsByElementValueTarget(StringBuilder jsBuilder, ValueElementTargetJsObjectName valueTargetElementJsObjectName, string parentValueObjectName, string valueObjectName, int indentCount)
        => valueTargetElementJsObjectName.AssociatedElementTarget.Insertion is null
            ? jsBuilder.Add(GenerateJsByDirectElementTarget, parentValueObjectName, valueObjectName, valueTargetElementJsObjectName.JsElementObjectName.Name, valueTargetElementJsObjectName.AssociatedElementTarget, indentCount)
            : jsBuilder.Add(GenerateJsByInsertingElementTarget, parentValueObjectName, valueObjectName, valueTargetElementJsObjectName.JsInsertionElementObjectName!, valueTargetElementJsObjectName.JsElementObjectName, valueTargetElementJsObjectName.AssociatedElementTarget, indentCount);

    private StringBuilder GenerateJsByDirectElementTarget(StringBuilder jsBuilder, string parentValueObjectName, string valueObjectName, string elementJsObjName, ElementValueTarget elementValueTarget, int indentCount)
        => jsBuilder.Append(codeFormatter.IndentLine(directUpdateValueJsGenerator.GenerateJs(elementJsObjName, elementValueTarget.Place, parentValueObjectName, valueObjectName), indentCount));

    private StringBuilder GenerateJsByInsertingElementTarget(StringBuilder jsBuilder, string parentValueObjectName, string valueObjectName, JsElementObjectName elementToInsertObjectName, JsElementObjectName elementsToAppendObjectName, ElementValueTarget elementValueTarget, int indentCount)
        => jsBuilder.Add(updateValueInsertJsGenerator.GenerateJs, elementValueTarget, elementToInsertObjectName, elementsToAppendObjectName, parentValueObjectName, valueObjectName, indentCount);

    private StringBuilder GenerateJsByCustomJsValueTarget(StringBuilder jsBuilder, CustomJsValueTarget customJsTarget, string parentValueObjectName, string valueObjName, int indentCount)
        => jsBuilder.Add(customJsJsGenerator.Generate, customJsTarget, $"{parentValueObjectName}.{valueObjName}", indentCount);

    private StringBuilder GenerateJsByOwnMappingTarget(StringBuilder jsBuilder, ValueOwnMappingTarget valueOwnMappingTarget, string parentValueObjectName, string valueObjectName, int indentCount)
    {
        var vmKey = viewModelKeyGenerator.Generate(valueOwnMappingTarget.AssociatedOwnMappingTarget.ObjectType);
        var vmArg = $"{parentValueObjectName}.{valueObjectName}";

        return jsBuilder.Append(codeFormatter.IndentLine($"{executeUpdateViewFunctionCall.Generate(vmKey, vmArg)};", indentCount));
    }
}
