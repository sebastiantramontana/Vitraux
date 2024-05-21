using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal class StoreElementsJsCodeBuilder(IStorageElementJsLineGenerator lineGenerator) : IStoreElementsJsCodeBuilder
{
    public string Build(IEnumerable<ElementObjectName> elements, string parentObjectName)
        => elements
            .Aggregate(new StringBuilder(), (sb, element) => sb.AppendLine(lineGenerator.Generate(element, parentObjectName)))
            .ToString()
            .Trim();
}
