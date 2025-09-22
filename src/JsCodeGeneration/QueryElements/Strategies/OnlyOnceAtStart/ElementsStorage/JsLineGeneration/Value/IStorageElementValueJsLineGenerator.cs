using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal interface IStorageElementValueJsLineGenerator
{
    string Generate(JsElementObjectName jsElementObjectName, string parentObjectName);
}
