using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsGenerator(
    IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCalling,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            ElementQuerySelectorString selector => BuildJsLineByQueryString(parentObjectName, jsElementObjectName.Name, selector.Query),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };

    private string BuildJsLineByQueryString(string parentObjectName, string jsElementObjectName, string query)
        => $"const {jsElementObjectName} = {getStoredElementsByQuerySelectorCalling.Generate(parentObjectName, query, jsElementObjectName)};";
}
