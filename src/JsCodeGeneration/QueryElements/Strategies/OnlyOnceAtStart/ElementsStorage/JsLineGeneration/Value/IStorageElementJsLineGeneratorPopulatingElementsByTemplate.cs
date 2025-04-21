using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal interface IStorageElementJsLineGeneratorPopulatingElementsByTemplate
{
    string Generate(InsertElementObjectName elementObjectName, string parentObjectToAppend);
}
