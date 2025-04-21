using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorPopulatingElementsByTemplate(
    IStorageElementJsLineGeneratorByTemplate storageElementJsLineGeneratorByTemplate,
    IStoragePopulatingElementJsLineGenerator storagePopulatingElementJsLineGenerator)
    : IStorageElementJsLineGeneratorPopulatingElementsByTemplate
{
    public string Generate(InsertElementObjectName templateObjectName, string parentObjectToAppend)
    {
        InsertElementTemplateSelectorId? templateSelector = templateObjectName!.AssociatedSelector as InsertElementTemplateSelectorId;
        string storedElementCall = storageElementJsLineGeneratorByTemplate.Generate(templateSelector!.TemplateId, templateObjectName.JsObjName);

        return storagePopulatingElementJsLineGenerator.Generate(storedElementCall, templateObjectName, parentObjectToAppend);
    }
}
