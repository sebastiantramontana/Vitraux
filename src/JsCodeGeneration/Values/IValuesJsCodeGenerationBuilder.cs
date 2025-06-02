namespace Vitraux.JsCodeGeneration.Values;

internal interface IValuesJsCodeGenerationBuilder
{
    string BuildJsCode(string parentObjectName, IEnumerable<ValueObjectNameWithJsTargets> values);
}
