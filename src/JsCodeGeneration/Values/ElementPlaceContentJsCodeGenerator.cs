using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

namespace Vitraux.JsCodeGeneration.Values;

internal class ElementPlaceContentJsCodeGenerator(ISetElementsContentCall setElementsContentCall) : IElementPlaceContentJsCodeGenerator
{
    public string Generate(string elementObjectName, string valueObjectName)
        => $"{setElementsContentCall.Generate(elementObjectName, $"vm.{valueObjectName}")};";
}
