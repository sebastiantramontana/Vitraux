using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageFromTemplateElementJsLineGenerator(
    IStorageElementJsLineGeneratorById generatorById,
    IStorageElementJsLineGeneratorByQuerySelector generatorByQuerySelector) : IStorageFromTemplateElementJsLineGenerator
{
    public string Generate(PopulatingAppendToElementSelector selector, string elementObjectName, string parentObjectName)
        => selector.SelectionBy switch
        {
            PopulatingAppendToElementSelection.Id => generatorById.Generate(elementObjectName, selector.Value),
            PopulatingAppendToElementSelection.QuerySelector => generatorByQuerySelector.Generate(elementObjectName, selector.Value, parentObjectName),
            _ => throw new NotImplementedException($"Selector type {selector.SelectionBy} not implemented"),
        };
}