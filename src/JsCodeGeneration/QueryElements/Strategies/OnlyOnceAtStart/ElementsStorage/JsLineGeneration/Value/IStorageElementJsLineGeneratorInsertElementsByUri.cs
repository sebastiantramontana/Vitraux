using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal interface IStorageElementJsLineGeneratorInsertElementsByUri
{
    string Generate(JsObjectName jsObjectName);
}
