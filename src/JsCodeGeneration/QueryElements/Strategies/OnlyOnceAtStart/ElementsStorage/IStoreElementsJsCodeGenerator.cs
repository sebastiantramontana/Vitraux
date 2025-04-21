using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal interface IStoreElementsJsCodeGenerator
{
    string Generate(IEnumerable<ElementObjectName> valueElements, IEnumerable<CollectionElementObjectName> collectionElementObjectNames, string parentObjectName);
}
