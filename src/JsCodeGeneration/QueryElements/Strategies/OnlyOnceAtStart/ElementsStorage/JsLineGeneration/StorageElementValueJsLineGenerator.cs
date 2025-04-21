using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementValueJsLineGenerator(
    IStorageElementJsLineGeneratorById generatorById,
    IStorageElementJsLineGeneratorByQuerySelector generatorByQuerySelector,
    IStorageElementJsLineGeneratorPopulatingElementsByTemplate generatorByTemplate,
    IStorageElementJsLineGeneratorPopulatingElementsByFetch generatorByFetch)
    : IStorageElementValueJsLineGenerator
{
    public string Generate(ElementObjectName elementObjectName, string parentObjectName)
        => elementObjectName.AssociatedSelector switch
        {
            ElementSelection.Id => generatorById.Generate(elementObjectName.JsObjName, (elementObjectName.AssociatedSelector as ElementIdSelectorIdString).Id),
            ElementSelection.QuerySelector => generatorByQuerySelector.Generate(elementObjectName.JsObjName, (elementObjectName.AssociatedSelector as ElementQuerySelectorString).Query, parentObjectName),
            ElementSelection.Template => generatorByTemplate.Generate((elementObjectName as InsertElementObjectName)!, parentObjectName),
            ElementSelection.Uri => generatorByFetch.Generate((elementObjectName as InsertElementObjectName)!, parentObjectName),
            _ => throw new NotImplementedException($"Selector type {elementObjectName.AssociatedSelector} not implemented in {GetType().FullName} for OnlyOnceAtStart initialization"),
        };
}

internal class StorageElementCollectionJsLineGenerator(
    IStorageElementJsLineGeneratorByTemplate storageElementJsLineGeneratorByTemplate,
    IStorageElementJsLineGeneratorByFetch storageElementJsLineGeneratorByFetch)
{
    public string Generate(CollectionElementObjectName elementObjectName, string parentObjectName)
    {
        return elementObjectName.AssociatedCollectionElementTarget.InsertionSelector.From switch
        {
            InsertionSelection.FromTemplate => GenerateByTemplate(elementObjectName, parentObjectName),
            InsertionSelection.FromFetch => throw new NotImplementedException(),
            _ => throw new NotImplementedException($"InsertionSelection type {elementObjectName.AssociatedCollectionElementTarget.InsertionSelector.From} not implemented in {GetType().FullName} for OnlyOnceAtStart initialization"),
        };
    }

    private string GenerateByTemplate(CollectionElementObjectName elementObjectName, string parentObjectName)
    {
        var templateId = (elementObjectName.AssociatedCollectionElementTarget.InsertionSelector as TemplateInsertionSelectorId)!.TemplateId;
        var templateElementObjectName = elementObjectName.InsertionElementName;

        return new StringBuilder()
            .AppendLine()
            .Append(storageElementJsLineGeneratorByTemplate.Generate(templateId, templateElementObjectName))
            .ToString();
    }

    private string GenerateByFetch(CollectionElementObjectName elementObjectName, string parentObjectName)
    {
        var uri = (elementObjectName.AssociatedCollectionElementTarget.InsertionSelector as UriInsertionSelectorUri)!.Uri;
        var templateElementObjectName = elementObjectName.InsertionElementName;

        return new StringBuilder()
            .AppendLine()
            .Append(storageElementJsLineGeneratorByFetch.Generate(templateId, templateElementObjectName))
            .ToString();
    }
}