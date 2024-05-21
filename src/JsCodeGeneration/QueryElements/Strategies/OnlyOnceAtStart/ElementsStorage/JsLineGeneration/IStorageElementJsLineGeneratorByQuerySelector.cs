namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal interface IStorageElementJsLineGeneratorByQuerySelector
{
    string Generate(string elementObjectName, string querySelector, string parentObjectName);
}
