using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal interface IStorageElementValueJsLineGenerator
{
    string Generate(JsObjectName jsObjectName, string parentObjectName);
}
