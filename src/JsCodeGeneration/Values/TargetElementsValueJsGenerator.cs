using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values.JsTargets;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementsValueJsGenerator(
    ITargetElementsDirectUpdateValueJsGenerator directUpdateValueJsGenerator,
    ITargetElementsUpdateValueInsertJsGenerator updateValueInsertJsGenerator,
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
            ValueOwnMappingTarget => GenerateJsByOwnMappingTarget(),
            _ => notImplementedCaseGuard.ThrowException<string>(target)
        };

    private string GenerateJsByElementValueTarget(ValueElementTargetJsObjectName valueTargetElementJsObjectName, string parentValueObjectName, string valueObjectName)
        => valueTargetElementJsObjectName.AssociatedElementTarget.Insertion is null
            ? GenerateJsByDirectElementTarget(parentValueObjectName, valueObjectName, valueTargetElementJsObjectName.JsElementObjectName.Name, valueTargetElementJsObjectName.AssociatedElementTarget)
            : GenerateJsByInsertingElementTarget(parentValueObjectName, valueObjectName, valueTargetElementJsObjectName.JsInsertionElementObjectName!, valueTargetElementJsObjectName.JsElementObjectName, valueTargetElementJsObjectName.AssociatedElementTarget);

    private string GenerateJsByDirectElementTarget(string parentValueObjectName, string valueObjectName, string elementJsObjName, ElementValueTarget elementValueTarget)
        => directUpdateValueJsGenerator.GenerateJs(elementJsObjName, elementValueTarget.Place, parentValueObjectName, valueObjectName);

    private string GenerateJsByInsertingElementTarget(string parentValueObjectName, string valueObjectName, JsObjectName elementToInsertObjectName, JsObjectName elementsToAppendObjectName, ElementValueTarget elementValueTarget)
        => updateValueInsertJsGenerator.GenerateJs(elementValueTarget, elementToInsertObjectName, elementsToAppendObjectName, parentValueObjectName, valueObjectName);

    private static string GenerateJsByCustomJsValueTarget(CustomJsValueTarget customJsTarget, string parentValueObjectName, string valueObjName)
        => $"/*{customJsTarget.FunctionName}({parentValueObjectName}.{valueObjName});*/";

    private static string GenerateJsByOwnMappingTarget()
        => string.Empty;
}
