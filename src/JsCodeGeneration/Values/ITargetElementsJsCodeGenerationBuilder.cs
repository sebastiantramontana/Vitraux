using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsJsCodeGenerationBuilder
{
    string Build(ValueObjectName value, IEnumerable<ElementObjectName> elements);
}
