using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeGenerator
{
    string GenerateJsCode(IEnumerable<ElementObjectName> elements, IEnumerable<CollectionElementObjectName> collectionElementObjectNames, string parentElementObjectName);
}
