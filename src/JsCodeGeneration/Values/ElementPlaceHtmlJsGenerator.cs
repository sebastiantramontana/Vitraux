using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

namespace Vitraux.JsCodeGeneration.Values;

internal class ElementPlaceHtmlJsGenerator(ISetElementsHtmlCall setElementsHtmlCall) : IElementPlaceHtmlJsGenerator
{
    public string Generate(string elementObjectName, string parentValueObjectName, string valueObjectName)
        => $"{setElementsHtmlCall.Generate(elementObjectName, $"{parentValueObjectName}.{valueObjectName}")};";
}
