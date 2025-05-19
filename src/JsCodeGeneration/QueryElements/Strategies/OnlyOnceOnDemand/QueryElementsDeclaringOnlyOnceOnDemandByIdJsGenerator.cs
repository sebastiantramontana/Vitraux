using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator(
    IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCalling,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementIdSelectorString selector => GenerateJsLineByIdString(jsObjectName.Name, selector.Id),
            ElementIdSelectorDelegate => string.Empty,
            _ => notImplementedSelector.ThrowException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsLineByIdString(string jsObjectName, string id)
        => $"const {jsObjectName} = {getStoredElementByIdAsArrayCalling.Generate(id, jsObjectName)};";
}
