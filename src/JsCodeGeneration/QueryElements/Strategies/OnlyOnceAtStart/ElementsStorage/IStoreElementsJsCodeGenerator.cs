using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal interface IStoreElementsJsCodeGenerator
{
    string Generate(IEnumerable<JsObjectName> jsObjectNames, string parentObjectName);
}
