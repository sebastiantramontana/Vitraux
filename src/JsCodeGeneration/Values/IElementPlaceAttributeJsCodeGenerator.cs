namespace Vitraux.JsCodeGeneration.Values;

internal interface IElementPlaceAttributeJsCodeGenerator
{
    string Generate(string attribute, string elementObjectName, string valueObjectName);
}
