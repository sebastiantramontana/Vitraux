using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementsValueJsGenerator(
    ITargetElementsDirectUpdateValueJsGenerator targetElementDirectJsCodeGenerator,
    ITargetByPopulatingElementsUpdateValueJsGenerator targetPopulatingJsCodeGenerator)
    : ITargetElementsValueJsGenerator
{
    public string GenerateJs(string parentValueObjectName, ValueObjectName value, IEnumerable<JsObjectName> jsObjectNames)
        => value
            .AssociatedValue
            .Targets
            .OfType<ElementValueTarget>()
            .Aggregate(new StringBuilder(), (sb, elementTarget) =>
            {
                var associatedJsObjects = GetObjectNamesAssociatedToTarget(jsObjectNames, elementTarget);
                var generator = GetCodeGeneratorBySelector(elementTarget);

                return sb
                    .AppendLine(generator.GenerateJsCode(elementTarget, associatedJsObjects, parentValueObjectName, value.Name))
                    .AppendLine();
            })
            .ToString()
            .TrimEnd();

    private IEnumerable<JsObjectName> GetObjectNamesAssociatedToTarget(IEnumerable<JsObjectName> jsObjectNames, ElementValueTarget elementTarget)
        => jsObjectNames.Where(e => e.AssociatedSelector == elementTarget.Selector);

    private ITargetElementsUpdateValueJsGenerator GetCodeGeneratorBySelector(ElementValueTarget elementTarget)
        => (elementTarget.Insertion is not null) ? targetPopulatingJsCodeGenerator : targetElementDirectJsCodeGenerator;
}
