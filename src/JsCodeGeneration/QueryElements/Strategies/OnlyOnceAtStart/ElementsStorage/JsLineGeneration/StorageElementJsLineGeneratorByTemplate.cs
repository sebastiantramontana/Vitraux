using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByTemplate(
    IGetStoredTemplateCall getStoredTemplateCall,
    IStoragePopulatingElementJsLineGenerator storagePopulatingElementJsLineGenerator)
    : IStorageElementJsLineGeneratorByTemplate
{
    public string Generate(PopulatingElementObjectName templateObjectName, string parentObjectToAppend)
    {
        ElementTemplateSelectorString? templateSelector = templateObjectName!.AssociatedSelector as ElementTemplateSelectorString;
        string storedElementCall = $"{getStoredTemplateCall.Generate(templateSelector!.TemplateId, templateObjectName.Name)};";

        return storagePopulatingElementJsLineGenerator.Generate(storedElementCall, templateObjectName, parentObjectToAppend);
    }
}
