using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal class OnlyOnceAtStartInitializeJsGenerator(
    IStorageElementValueJsLineGenerator valueJsLineGenerator,
    IStorageElementCollectionJsLineGenerator collectionJsLineGenerator,
    IPromiseJsGenerator promiseJsGenerator,
    INotImplementedCaseGuard notImplementedSelector)
    : IOnlyOnceAtStartInitializeJsGenerator
{
    public string GenerateJs(IEnumerable<JsElementObjectName> jsObjectNames, string parentObjectName)
        => GenerateJsLines(new StringBuilder(), jsObjectNames, parentObjectName)
            .Append(promiseJsGenerator.ReturnResolvedPromiseJsLine)
            .ToString();

    private StringBuilder GenerateJsLines(StringBuilder stringBuilder, IEnumerable<JsElementObjectName> jsObjectNames, string parentObjectName)
        => jsObjectNames
            .Aggregate(stringBuilder, (sb, jsElementObjectName) => sb.AppendLine(GenerateJsLine(jsElementObjectName, parentObjectName)));

    private string GenerateJsLine(JsElementObjectName jsElementObjectName, string parentObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            ElementSelectorBase or InsertElementSelectorBase => valueJsLineGenerator.Generate(jsElementObjectName, parentObjectName),
            InsertionSelectorBase => collectionJsLineGenerator.Generate(jsElementObjectName),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };
}