namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal interface IStorageElementJsLineGeneratorByUri
{
    string Generate(Uri uri, string fetchedJsObjectName);
}
