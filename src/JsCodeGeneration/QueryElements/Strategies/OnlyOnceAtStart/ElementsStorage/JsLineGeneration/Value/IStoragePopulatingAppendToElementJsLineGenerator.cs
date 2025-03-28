using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal interface IStoragePopulatingAppendToElementJsLineGenerator
{
    string Generate(PopulatingAppendToElementSelectorBase selector, string elementObjectName, string parentObjectName);
}