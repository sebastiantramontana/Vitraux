using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal interface IStorageElementJsLineGeneratorElementsByQuery
{
    string Generate(JsElementObjectName jsElementObjectName, string parentObjectName);
}
