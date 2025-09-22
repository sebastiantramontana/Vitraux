using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.Values.JsTargets;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValueNamesGenerator(INotImplementedCaseGuard notImplementedCaseGuard) : IValueNamesGenerator
{
    const string ValueObjectNamePrefix = "v";

    public IEnumerable<FullValueObjectName> Generate(IEnumerable<ValueData> values, IEnumerable<JsElementObjectName> currentElementJsObjectNames)
        => values.Select((value, indexAsPostfix) => new FullValueObjectName(GenerateObjName(indexAsPostfix), GenerateJsTargets(value.Targets, currentElementJsObjectNames), value));

    private IEnumerable<ValueJsTarget> GenerateJsTargets(IEnumerable<IValueTarget> targets, IEnumerable<JsElementObjectName> currentElementJsObjectNames)
        => targets.Select(target => MapValueTargetToJs(target, currentElementJsObjectNames));

    private ValueJsTarget MapValueTargetToJs(IValueTarget target, IEnumerable<JsElementObjectName> currentElementJsObjectNames)
        => target switch
        {
            ElementValueTarget elementTarget => CreateValueElementTargetJsObjectName(elementTarget, currentElementJsObjectNames),
            CustomJsValueTarget customJsTarget => new ValueCustomJsTarget(customJsTarget),
            OwnMappingTarget ownMappingTarget => new ValueOwnMappingTarget(ownMappingTarget),
            _ => notImplementedCaseGuard.ThrowException<ValueJsTarget>(target)
        };

    private static ValueElementTargetJsObjectName CreateValueElementTargetJsObjectName(ElementValueTarget elementTarget, IEnumerable<JsElementObjectName> currentElementJsObjectNames)
    {
        var jsElementObjectName = currentElementJsObjectNames.Single(e => e.AssociatedSelector == elementTarget.Selector);

        var jsInsertionElementObjectName = elementTarget.Insertion is not null
            ? currentElementJsObjectNames.Single(e => e.AssociatedSelector == elementTarget.Insertion)
            : null;

        return new ValueElementTargetJsObjectName(elementTarget, jsElementObjectName, jsInsertionElementObjectName);
    }

    private static string GenerateObjName(int indexAsPostfix)
        => $"{ValueObjectNamePrefix}{indexAsPostfix}";
}
