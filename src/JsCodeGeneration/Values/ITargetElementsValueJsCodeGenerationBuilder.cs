using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsValueJsCodeGenerationBuilder
{
    string Build(ValueObjectName value, IEnumerable<ElementObjectName> elements);
}
