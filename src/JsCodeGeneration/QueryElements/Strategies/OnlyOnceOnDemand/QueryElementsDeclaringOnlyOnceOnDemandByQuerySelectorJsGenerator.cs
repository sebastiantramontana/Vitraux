using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsGenerator(
    IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCalling,
    INotImplementedSelector notImplementedSelector)
    : IQueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementQuerySelectorString selector => BuildJsLineByQueryString(parentObjectName, jsObjectName.Name, selector.Query),
            ElementQuerySelectorDelegate => string.Empty,
            _ => notImplementedSelector.ThrowNotImplementedException<string>(jsObjectName.AssociatedSelector)
        };

    private string BuildJsLineByQueryString(string parentObjectName, string jsObjectName, string query)
        => $"const {jsObjectName} = {getStoredElementsByQuerySelectorCalling.Generate(parentObjectName, query, jsObjectName)};";
}
