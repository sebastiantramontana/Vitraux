using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator(
    IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCalling,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            ElementIdSelectorString selector => GenerateJsLineByIdString(jsElementObjectName.Name, selector.Id),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };

    private string GenerateJsLineByIdString(string jsElementObjectName, string id)
        => $"const {jsElementObjectName} = {getStoredElementByIdAsArrayCalling.Generate(id, jsElementObjectName)};";
}
