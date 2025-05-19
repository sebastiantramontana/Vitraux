namespace Vitraux.JsCodeGeneration.Values;

internal interface IElementPlaceAttributeJsGenerator
{
    string Generate(string attribute, string elementObjectName, string parentValueObjectName, string valueObjectName);
}
