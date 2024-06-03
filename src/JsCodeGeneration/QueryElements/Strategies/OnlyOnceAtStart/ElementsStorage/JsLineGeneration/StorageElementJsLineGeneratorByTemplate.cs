using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByTemplate(
    IGetStoredTemplateCall getStoredElementByTemplateAsArrayCall,
    IStorageFromTemplateElementJsLineGenerator storageFromTemplateElementJsLineGenerator)
    : IStorageElementJsLineGeneratorByTemplate
{
    public string Generate(ElementObjectName elementObjectName, string parentObjectToAppend)
    {
        var templateObjectName = elementObjectName as ElementTemplateObjectName;
        var templateSelector = templateObjectName!.AssociatedSelector as ElementTemplateSelectorString;

        return new StringBuilder()
            .AppendLine($"{getStoredElementByTemplateAsArrayCall.Generate(templateSelector.TemplateId, elementObjectName.Name)};")
            .Append(storageFromTemplateElementJsLineGenerator.Generate(templateSelector!.ElementToAppend, templateObjectName.AppendToName, parentObjectToAppend))
            .ToString();
    }
}
