using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByTemplate(IGetStoredTemplateCall getStoredTemplateCall)
    : IStorageElementJsLineGeneratorByTemplate
{
    public string Generate(string templateId, string elementObjectName)
        => $"{getStoredTemplateCall.Generate(templateId, elementObjectName)};";
}
