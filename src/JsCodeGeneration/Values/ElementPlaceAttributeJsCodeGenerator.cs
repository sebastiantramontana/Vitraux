using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

namespace Vitraux.JsCodeGeneration.Values;

internal class ElementPlaceAttributeJsCodeGenerator(ISetElementsAttributeCall setElementsAttributeCall) : IElementPlaceAttributeJsCodeGenerator
{
    public string Generate(string attribute, string elementObjectName, string valueObjectName)
        => $"{setElementsAttributeCall.Generate(elementObjectName, attribute, $"vm.{valueObjectName}")};";
}
