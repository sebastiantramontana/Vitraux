using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal interface IValuesJsCodeGenerationBuilder
{
    string Build(IEnumerable<ValueObjectName> values, IEnumerable<ElementObjectName> elements);
}
