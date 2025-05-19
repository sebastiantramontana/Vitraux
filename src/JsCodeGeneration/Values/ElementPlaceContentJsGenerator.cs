using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

namespace Vitraux.JsCodeGeneration.Values;

internal class ElementPlaceContentJsGenerator(ISetElementsContentCall setElementsContentCall) : IElementPlaceContentJsGenerator
{
    public string Generate(string elementObjectName, string parentValueObjectName, string valueObjectName)
        => $"{setElementsContentCall.Generate(elementObjectName, $"{parentValueObjectName}.{valueObjectName}")};";
}
