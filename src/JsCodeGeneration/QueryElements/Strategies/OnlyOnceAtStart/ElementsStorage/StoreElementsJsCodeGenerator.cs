using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal class StoreElementsJsCodeGenerator(IStorageElementValueJsLineGenerator lineGenerator) : IStoreElementsJsCodeGenerator
{
    public string Generate(IEnumerable<ElementObjectName> valueElements, IEnumerable<CollectionElementObjectName> collectionElementObjectNames, string parentObjectName)
        => GenerateValuesElementJsCode(new StringBuilder(), valueElements, parentObjectName)
            .ToString()
            .TrimEnd();

    private StringBuilder GenerateValuesElementJsCode(StringBuilder stringBuilder, IEnumerable<ElementObjectName> valueElements, string parentObjectName)
        => valueElements
            .Aggregate(stringBuilder, (sb, element) => sb.AppendLine(lineGenerator.Generate(element, parentObjectName)));

    private StringBuilder GenerateCollectionsElementJsCode(StringBuilder stringBuilder, IEnumerable<CollectionElementObjectName> collectionElementObjectNames, string parentObjectName)
        => collectionElementObjectNames
            .Aggregate(stringBuilder, (sb, element) => sb.AppendLine(lineGenerator.Generate(element, parentObjectName)));

}
