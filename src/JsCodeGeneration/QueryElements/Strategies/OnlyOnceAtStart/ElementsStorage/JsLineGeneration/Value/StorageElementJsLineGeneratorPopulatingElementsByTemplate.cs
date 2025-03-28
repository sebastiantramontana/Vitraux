using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorPopulatingElementsByTemplate(
    IStorageElementJsLineGeneratorByTemplate storageElementJsLineGeneratorByTemplate,
    IStoragePopulatingElementJsLineGenerator storagePopulatingElementJsLineGenerator)
    : IStorageElementJsLineGeneratorPopulatingElementsByTemplate
{
    public string Generate(PopulatingElementObjectName templateObjectName, string parentObjectToAppend)
    {
        ElementTemplateSelectorString? templateSelector = templateObjectName!.AssociatedSelector as ElementTemplateSelectorString;
        string storedElementCall = storageElementJsLineGeneratorByTemplate.Generate(templateSelector!.TemplateId, templateObjectName.Name);

        return storagePopulatingElementJsLineGenerator.Generate(storedElementCall, templateObjectName, parentObjectToAppend);
    }
}
