using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

namespace Vitraux.JsCodeGeneration.Values;

internal class ElementPlaceAttributeJsCodeGenerator(ISetElementsAttributeCall setElementsAttributeCall) : IElementPlaceAttributeJsCodeGenerator
{
    public string Generate(string attribute, string elementObjectName, string parentValueObjectName, string valueObjectName)
        => $"{setElementsAttributeCall.Generate(elementObjectName, attribute, $"{parentValueObjectName}.{valueObjectName}")};";
}
