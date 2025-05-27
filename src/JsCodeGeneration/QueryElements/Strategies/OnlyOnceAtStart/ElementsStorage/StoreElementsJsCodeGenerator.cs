using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal class StoreElementsJsCodeGenerator(
    IStorageElementValueJsLineGenerator valueJsLineGenerator,
    IStorageElementCollectionJsLineGenerator collectionJsLineGenerator,
    IPromiseJsGenerator promiseJsGenerator,
    INotImplementedCaseGuard notImplementedSelector)
    : IStoreElementsJsCodeGenerator
{
    public string Generate(IEnumerable<JsObjectName> jsObjectNames, string parentObjectName)
        => GenerateJsLines(new StringBuilder(), jsObjectNames, parentObjectName)
            .Append(promiseJsGenerator.ReturnResolvedPromiseJsLine)
            .ToString()
            .TrimEnd();

    private StringBuilder GenerateJsLines(StringBuilder stringBuilder, IEnumerable<JsObjectName> jsObjectNames, string parentObjectName)
        => jsObjectNames
            .Aggregate(stringBuilder, (sb, jsObjectName) => sb.AppendLine(GenerateJsLine(jsObjectName, parentObjectName)));

    private string GenerateJsLine(JsObjectName jsObjectName, string parentObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementSelectorBase or InsertElementSelectorBase => valueJsLineGenerator.Generate(jsObjectName, parentObjectName),
            InsertionSelectorBase => collectionJsLineGenerator.Generate(jsObjectName),
            _ => notImplementedSelector.ThrowException<string>(jsObjectName.AssociatedSelector)
        };
}