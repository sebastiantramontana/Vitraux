namespace Vitraux.JsCodeGeneration.Values;

internal interface IValuesJsCodeGenerationBuilder
{
    BuiltValueJs BuildJsCode(string parentObjectName, IEnumerable<FullValueObjectName> values);
}
