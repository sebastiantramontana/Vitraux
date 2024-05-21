using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal interface IStorageElementJsLineGeneratorByTemplate
{
    string Generate(ElementObjectName elementObjectName, string parentObjectToAppend);
}
