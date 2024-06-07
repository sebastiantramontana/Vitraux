using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal interface IStoragePopulatingAppendToElementJsLineGenerator
{
    string Generate(PopulatingAppendToElementSelectorBase selector, string elementObjectName, string parentObjectName);
}