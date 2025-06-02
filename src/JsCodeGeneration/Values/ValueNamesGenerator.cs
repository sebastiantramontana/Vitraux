using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values.JsTargets;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValueNamesGenerator(INotImplementedCaseGuard notImplementedCaseGuard) : IValueNamesGenerator
{
    const string ValueObjectNamePrefix = "v";

    IEnumerable<FullValueObjectName> IValueNamesGenerator.Generate(IEnumerable<ValueData> values, IEnumerable<JsObjectName> allElementJsObjectNames)
        => values.Select((value, indexAsPostfix) => new FullValueObjectName(GenerateObjName(indexAsPostfix), GenerateJsTargets(value.Targets, allElementJsObjectNames), value));

    private IEnumerable<ValueJsTarget> GenerateJsTargets(IEnumerable<IValueTarget> targets, IEnumerable<JsObjectName> allElementJsObjectNames)
        => targets.Select(target => MapValueTargetToJs(target, allElementJsObjectNames));

    private ValueJsTarget MapValueTargetToJs(IValueTarget target, IEnumerable<JsObjectName> allElementJsObjectNames)
        => target switch
        {
            ElementValueTarget elementTarget => CreateValueElementTargetJsObjectName(elementTarget, allElementJsObjectNames),
            CustomJsValueTarget customJsTarget => new ValueCustomJsTarget(customJsTarget),
            OwnMappingTarget ownMappingTarget => new ValueOwnMappingTarget(ownMappingTarget),
            _ => notImplementedCaseGuard.ThrowException<ValueJsTarget>(target)
        };

    private static ValueElementTargetJsObjectName CreateValueElementTargetJsObjectName(ElementValueTarget elementTarget, IEnumerable<JsObjectName> allElementJsObjectNames)
    {
        var jsElementObjectName = allElementJsObjectNames.Single(e => e.AssociatedSelector == elementTarget.Selector);

        var jsInsertionElementObjectName = elementTarget.Insertion is not null
            ? allElementJsObjectNames.Single(e => e.AssociatedSelector == elementTarget.Insertion)
            : null;

        return new ValueElementTargetJsObjectName(elementTarget, jsElementObjectName, jsInsertionElementObjectName);
    }

    private static string GenerateObjName(int indexAsPostfix)
        => $"{ValueObjectNamePrefix}{indexAsPostfix}";
}
