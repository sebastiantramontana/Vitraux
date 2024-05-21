using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration
{
    internal interface IStorageFromTemplateElementJsLineGenerator
    {
        string Generate(PopulatingAppendToElementSelector selector, string elementObjectName, string parentObjectName);
    }
}