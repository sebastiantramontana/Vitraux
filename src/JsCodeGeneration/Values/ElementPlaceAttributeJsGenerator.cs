using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

namespace Vitraux.JsCodeGeneration.Values;

internal class ElementPlaceAttributeJsGenerator(ISetElementsAttributeCall setElementsAttributeCall) : IElementPlaceAttributeJsGenerator
{
    public string Generate(string attribute, string elementObjectName, string parentValueObjectName, string valueObjectName)
        => $"{setElementsAttributeCall.Generate(elementObjectName, attribute, $"{parentValueObjectName}.{valueObjectName}")};";
}
