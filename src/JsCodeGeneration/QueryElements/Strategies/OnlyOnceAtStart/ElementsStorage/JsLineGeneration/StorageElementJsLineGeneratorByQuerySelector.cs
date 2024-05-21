using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByQuerySelector(IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCalling) : IStorageElementJsLineGeneratorByQuerySelector
{
    public string Generate(string elementObjectName, string querySelector, string parentObjectName)
        => $"{getStoredElementsByQuerySelectorCalling.Generate(parentObjectName, querySelector, elementObjectName)};";
}
