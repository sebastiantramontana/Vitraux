using System.Text;
using Vitraux.Execution.ViewModelNames;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.CustomJsGeneration;
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
    INotImplementedCaseGuard notImplementedCaseGuard)
    : ITargetElementsValueJsGenerator
{
    public string GenerateJs(string parentValueObjectName, ValueObjectNameWithJsTargets valueObjectNameWithTargets)
        => valueObjectNameWithTargets.JsTargets
            .Aggregate(new StringBuilder(), (sb, target) =>
            {
                var jsCode = GenerateJsCodeByJsTarget(target, parentValueObjectName, valueObjectNameWithTargets.Name);
                return sb.AppendLine(jsCode);
            })
            .ToString()
            .TrimEnd();

    private string GenerateJsCodeByJsTarget(ValueJsTarget target, string parentValueObjectName, string valueObjName)
        => target switch
        {
            ValueElementTargetJsObjectName valueJsElementTarget => GenerateJsByElementValueTarget(valueJsElementTarget, parentValueObjectName, valueObjName),
            ValueCustomJsTarget customJsTarget => GenerateJsByCustomJsValueTarget(customJsTarget.AssociatedCustomJsTarget, parentValueObjectName, valueObjName),
            ValueOwnMappingTarget valueOwnMappingTarget => GenerateJsByOwnMappingTarget(valueOwnMappingTarget, parentValueObjectName, valueObjName),
            _ => notImplementedCaseGuard.ThrowException<string>(target)
        };

    private string GenerateJsByElementValueTarget(ValueElementTargetJsObjectName valueTargetElementJsObjectName, string parentValueObjectName, string valueObjectName)
        => valueTargetElementJsObjectName.AssociatedElementTarget.Insertion is null
            ? GenerateJsByDirectElementTarget(parentValueObjectName, valueObjectName, valueTargetElementJsObjectName.JsElementObjectName.Name, valueTargetElementJsObjectName.AssociatedElementTarget)
            : GenerateJsByInsertingElementTarget(parentValueObjectName, valueObjectName, valueTargetElementJsObjectName.JsInsertionElementObjectName!, valueTargetElementJsObjectName.JsElementObjectName, valueTargetElementJsObjectName.AssociatedElementTarget);

    private string GenerateJsByDirectElementTarget(string parentValueObjectName, string valueObjectName, string elementJsObjName, ElementValueTarget elementValueTarget)
        => directUpdateValueJsGenerator.GenerateJs(elementJsObjName, elementValueTarget.Place, parentValueObjectName, valueObjectName);

    private string GenerateJsByInsertingElementTarget(string parentValueObjectName, string valueObjectName, JsElementObjectName elementToInsertObjectName, JsElementObjectName elementsToAppendObjectName, ElementValueTarget elementValueTarget)
        => updateValueInsertJsGenerator.GenerateJs(elementValueTarget, elementToInsertObjectName, elementsToAppendObjectName, parentValueObjectName, valueObjectName);

    private string GenerateJsByCustomJsValueTarget(CustomJsValueTarget customJsTarget, string parentValueObjectName, string valueObjName)
        => customJsJsGenerator.Generate(customJsTarget, $"{parentValueObjectName}.{valueObjName}");

    private string GenerateJsByOwnMappingTarget(ValueOwnMappingTarget valueOwnMappingTarget, string parentValueObjectName, string valueObjectName)
    {
        var vmKey = viewModelKeyGenerator.Generate(valueOwnMappingTarget.AssociatedOwnMappingTarget.ObjectType);
        var vmArg = $"{parentValueObjectName}.{valueObjectName}";

        return $"{executeUpdateViewFunctionCall.Generate(vmKey, vmArg)};";
    }
}
