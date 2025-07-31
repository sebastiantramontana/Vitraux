using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByQuerySelectorJsGenerator(
    IGetElementsByQuerySelectorCall getElementsByQuerySelectorCalling,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringAlwaysByQuerySelectorJsGenerator
{
    public string GenerateJsCode(string parentElementObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementQuerySelectorString elementQuerySelector => GenerateJsLineByQueryString(jsObjectName.Name, parentElementObjectName, elementQuerySelector.Query),
            _ => notImplementedSelector.ThrowException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsLineByQueryString(string jsObjectName, string parentElementObjectName, string query)
        => $"const {jsObjectName} = {getElementsByQuerySelectorCalling.Generate(parentElementObjectName, query)};";
}
