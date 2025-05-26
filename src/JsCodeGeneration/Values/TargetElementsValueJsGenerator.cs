using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementsValueJsGenerator(
    ITargetElementsDirectUpdateValueJsGenerator directUpdateValueJsGenerator,
    ITargetElementsUpdateValueInsertJsGenerator updateValueInsertJsGenerator,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : ITargetElementsValueJsGenerator
{
    public string GenerateJs(string parentValueObjectName, ValueObjectName valueObjectName, IEnumerable<JsObjectName> jsObjectNames)
        => valueObjectName
            .AssociatedValue
            .Targets
            .Aggregate(new StringBuilder(), (sb, target) =>
            {
                var jsCode = target switch
                {
                    ElementValueTarget elementValueTarget => GenerateJsByElementValueTarget(elementValueTarget, parentValueObjectName, valueObjectName.Name, jsObjectNames),
                    CustomJsValueTarget customJsTarget => GenerateJsByCustomJsValueTarget(customJsTarget, parentValueObjectName, valueObjectName.Name),
                    OwnMappingTarget => GenerateJsByOwnMappingTarget(),
                    _ => notImplementedCaseGuard.ThrowException<string>(target)
                };

                return sb.AppendLine(jsCode);
            })
            .ToString()
            .TrimEnd();

    private string GenerateJsByElementValueTarget(ElementValueTarget elementValueTarget, string parentValueObjectName, string valueObjectName, IEnumerable<JsObjectName> jsObjectNames)
        => elementValueTarget.Insertion is null
            ? GenerateJsByDirectElementTarget(parentValueObjectName, valueObjectName, elementValueTarget, jsObjectNames)
            : GenerateJsByInsertingElementTarget(parentValueObjectName, valueObjectName, elementValueTarget, jsObjectNames);

    private string GenerateJsByDirectElementTarget(string parentValueObjectName, string valueObjectName, ElementValueTarget elementValueTarget, IEnumerable<JsObjectName> jsObjectNames)
    {
        var jsObjectName = SearchObjectNameBySelector(jsObjectNames, elementValueTarget.Selector);
        return directUpdateValueJsGenerator.GenerateJs(jsObjectName.Name, elementValueTarget.Place, parentValueObjectName, valueObjectName);
    }

    private string GenerateJsByInsertingElementTarget(string parentValueObjectName, string valueObjectName, ElementValueTarget elementValueTarget, IEnumerable<JsObjectName> jsObjectNames)
    {
        var elementsToAppendObjectName = SearchObjectNameBySelector(jsObjectNames, elementValueTarget.Selector);
        var elementToInsertObjectName = SearchObjectNameBySelector(jsObjectNames, elementValueTarget.Insertion!);

        return updateValueInsertJsGenerator.GenerateJs(elementValueTarget, elementToInsertObjectName, elementsToAppendObjectName, parentValueObjectName, valueObjectName);
    }

    private static string GenerateJsByCustomJsValueTarget(CustomJsValueTarget customJsTarget, string parentValueObjectName, string valueObjName)
        => $"/*{customJsTarget.FunctionName}({parentValueObjectName}.{valueObjName});*/";

    private static string GenerateJsByOwnMappingTarget()
        => string.Empty;

    private static JsObjectName SearchObjectNameBySelector(IEnumerable<JsObjectName> jsObjectNames, SelectorBase selector)
        => jsObjectNames.SingleOrDefault(o => o.AssociatedSelector == selector)
            ?? throw new InvalidOperationException($"No object name found for selector {selector}.");
}
