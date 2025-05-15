namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal interface IStorageElementJsLineGeneratorById
{
    string Generate(string jsObjectName, string id);
}
