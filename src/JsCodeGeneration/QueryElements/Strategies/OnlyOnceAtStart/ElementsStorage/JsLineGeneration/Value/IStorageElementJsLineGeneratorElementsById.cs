using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal interface IStorageElementJsLineGeneratorElementsById
{
    string Generate(JsElementObjectName jsElementObjectName);
}
