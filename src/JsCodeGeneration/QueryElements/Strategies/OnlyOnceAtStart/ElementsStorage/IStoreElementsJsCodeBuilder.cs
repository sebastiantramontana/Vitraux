using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal interface IStoreElementsJsCodeBuilder
{
    string Build(IEnumerable<ElementObjectName> elements, IEnumerable<CollectionElementObjectName> collectionElementObjectNames, string parentObjectName);
}
