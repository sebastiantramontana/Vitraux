namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsValueJsGenerator
{
    string GenerateJs(string parentValueObjectName, ValueObjectNameWithJsTargets valueObjectNameWithTargets);
}
