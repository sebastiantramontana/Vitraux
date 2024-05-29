using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageFromTemplateElementJsLineGenerator(
    IStorageElementJsLineGeneratorById generatorById,
    IStorageElementJsLineGeneratorByQuerySelector generatorByQuerySelector) : IStorageFromTemplateElementJsLineGenerator
{
    public string Generate(PopulatingAppendToElementSelectorBase selector, string elementObjectName, string parentObjectName)
        => selector.SelectionBy switch
        {
            PopulatingAppendToElementSelection.Id => generatorById.Generate(elementObjectName, (selector as PopulatingAppendToElementIdSelectorString).Id),
            PopulatingAppendToElementSelection.QuerySelector => generatorByQuerySelector.Generate(elementObjectName, (selector as PopulatingAppendToElementQuerySelectorString).Query, parentObjectName),
            _ => throw new NotImplementedException($"Selector type {selector.SelectionBy} not implemented"),
        };
}