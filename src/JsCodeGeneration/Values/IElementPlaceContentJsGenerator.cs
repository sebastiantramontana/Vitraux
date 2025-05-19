namespace Vitraux.JsCodeGeneration.Values;

internal interface IElementPlaceContentJsGenerator
{
    string Generate(string elementObjectName, string parentValueObjectName, string valueObjectName);
}
