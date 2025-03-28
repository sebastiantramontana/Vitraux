using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.Modeling.Data.Selectors.Elements;
using Vitraux.Modeling.Data.Selectors.Insertion;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementValueJsLineGenerator(
    IStorageElementJsLineGeneratorById generatorById,
    IStorageElementJsLineGeneratorByQuerySelector generatorByQuerySelector,
    IStorageElementJsLineGeneratorPopulatingElementsByTemplate generatorByTemplate,
    IStorageElementJsLineGeneratorPopulatingElementsByFetch generatorByFetch)
    : IStorageElementValueJsLineGenerator
{
    public string Generate(ElementObjectName elementObjectName, string parentObjectName)
        => elementObjectName.AssociatedSelector.SelectionBy switch
        {
            ElementSelection.Id => generatorById.Generate(elementObjectName.Name, (elementObjectName.AssociatedSelector as ElementIdSelectorString).Id),
            ElementSelection.QuerySelector => generatorByQuerySelector.Generate(elementObjectName.Name, (elementObjectName.AssociatedSelector as ElementQuerySelectorString).Query, parentObjectName),
            ElementSelection.Template => generatorByTemplate.Generate((elementObjectName as PopulatingElementObjectName)!, parentObjectName),
            ElementSelection.Uri => generatorByFetch.Generate((elementObjectName as PopulatingElementObjectName)!, parentObjectName),
            _ => throw new NotImplementedException($"Selector type {elementObjectName.AssociatedSelector.SelectionBy} not implemented in {GetType().FullName} for OnlyOnceAtStart initialization"),
        };
}

internal class StorageElementCollectionJsLineGenerator(
    IStorageElementJsLineGeneratorByTemplate storageElementJsLineGeneratorByTemplate,
    IStorageElementJsLineGeneratorByFetch storageElementJsLineGeneratorByFetch)
{
    public string Generate(CollectionElementObjectName elementObjectName, string parentObjectName)
    {
        return elementObjectName.AssociatedCollectionSelector.InsertionSelector.From switch
        {
            InsertionSelection.FromTemplate => GenerateByTemplate(elementObjectName, parentObjectName),
            InsertionSelection.FromFetch => throw new NotImplementedException(),
            _ => throw new NotImplementedException($"InsertionSelection type {elementObjectName.AssociatedCollectionSelector.InsertionSelector.From} not implemented in {GetType().FullName} for OnlyOnceAtStart initialization"),
        };
    }

    private string GenerateByTemplate(CollectionElementObjectName elementObjectName, string parentObjectName)
    {
        var templateId = (elementObjectName.AssociatedCollectionSelector.InsertionSelector as TemplateInsertionSelectorId)!.TemplateId;
        var templateElementObjectName = elementObjectName.InsertionElementName;

        return new StringBuilder()
            .AppendLine()
            .Append(storageElementJsLineGeneratorByTemplate.Generate(templateId, templateElementObjectName))
            .ToString();
    }

    private string GenerateByFetch(CollectionElementObjectName elementObjectName, string parentObjectName)
    {
        var uri = (elementObjectName.AssociatedCollectionSelector.InsertionSelector as FetchInsertionSelectorUri)!.Uri;
        var templateElementObjectName = elementObjectName.InsertionElementName;

        return new StringBuilder()
            .AppendLine()
            .Append(storageElementJsLineGeneratorByFetch.Generate(templateId, templateElementObjectName))
            .ToString();
    }
}