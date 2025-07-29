namespace Vitraux.JsCodeGeneration.Values;

internal interface IElementPlaceHtmlJsGenerator
{
    string Generate(string elementObjectName, string parentValueObjectName, string valueObjectName);
}
