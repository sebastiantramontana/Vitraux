using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal interface IQueryElementsOnlyOnceAtStartup
{
    void StoreElementsInAdvance(IEnumerable<JsObjectName> jsObjectNames, string parentObjectName);
}
