using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByQuerySelectorJsGenerator(
    IGetElementsByQuerySelectorCall getElementsByQuerySelectorCalling,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringAlwaysByQuerySelectorJsGenerator
{
    public string GenerateJsCode(string parentElementObjectName, JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            ElementQuerySelectorString elementQuerySelector => GenerateJsLineByQueryString(jsElementObjectName.Name, parentElementObjectName, elementQuerySelector.Query),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };

    private string GenerateJsLineByQueryString(string jsElementObjectName, string parentElementObjectName, string query)
        => $"const {jsElementObjectName} = {getElementsByQuerySelectorCalling.Generate(parentElementObjectName, query)};";
}
