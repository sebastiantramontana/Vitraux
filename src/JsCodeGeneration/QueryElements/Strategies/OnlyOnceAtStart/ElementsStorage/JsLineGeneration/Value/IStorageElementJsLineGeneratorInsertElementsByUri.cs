using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal interface IStorageElementJsLineGeneratorInsertElementsByUri
{
    string Generate(JsElementObjectName jsElementObjectName);
}
