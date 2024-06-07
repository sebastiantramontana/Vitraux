using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGenerator(
    IStorageElementJsLineGeneratorById generatorById,
    IStorageElementJsLineGeneratorByQuerySelector generatorByQuerySelector,
    IStorageElementJsLineGeneratorByTemplate generatorByTemplate,
    IStorageElementJsLineGeneratorByFetch generatorByFetch)
    : IStorageElementJsLineGenerator
{
    public string Generate(ElementObjectName elementObjectName, string parentObjectName)
        => elementObjectName.AssociatedSelector.SelectionBy switch
        {
            ElementSelection.Id => generatorById.Generate(elementObjectName.Name, (elementObjectName.AssociatedSelector as ElementIdSelectorString).Id),
            ElementSelection.QuerySelector => generatorByQuerySelector.Generate(elementObjectName.Name, (elementObjectName.AssociatedSelector as ElementQuerySelectorString).Query, parentObjectName),
            ElementSelection.Template => generatorByTemplate.Generate((elementObjectName as PopulatingElementObjectName)!, parentObjectName),
            ElementSelection.Fetch => generatorByFetch.Generate((elementObjectName as PopulatingElementObjectName)!, parentObjectName),
            _ => throw new NotImplementedException($"Selector type {elementObjectName.AssociatedSelector.SelectionBy} not implemented in {GetType().FullName} for OnlyOnceAtStart initialization"),
        };
}