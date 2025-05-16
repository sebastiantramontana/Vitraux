using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal class StoreElementsJsCodeGenerator(
    IStorageElementValueJsLineGenerator valueJsLineGenerator,
    IStorageElementCollectionJsLineGenerator collectionJsLineGenerator,
    INotImplementedSelector notImplementedSelector)
    : IStoreElementsJsCodeGenerator
{
    public string Generate(IEnumerable<JsObjectName> jsObjectNames, string parentObjectName)
        => GenerateJsLines(new StringBuilder(), jsObjectNames, parentObjectName)
            .ToString()
            .TrimEnd();

    private StringBuilder GenerateJsLines(StringBuilder stringBuilder, IEnumerable<JsObjectName> jsObjectNames, string parentObjectName)
        => jsObjectNames
            .Aggregate(stringBuilder, (sb, jsObjectName) => sb.AppendLine(GenerateJsLine(jsObjectName, parentObjectName)));

    private string GenerateJsLine(JsObjectName jsObjectName, string parentObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementSelectorBase => valueJsLineGenerator.Generate(jsObjectName, parentObjectName),
            InsertionSelectorBase => collectionJsLineGenerator.Generate(jsObjectName),
            _ => notImplementedSelector.ThrowNotImplementedException<string>(jsObjectName.AssociatedSelector)
        };
}