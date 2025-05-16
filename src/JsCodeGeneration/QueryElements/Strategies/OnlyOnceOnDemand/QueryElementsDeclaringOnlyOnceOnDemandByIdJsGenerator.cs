using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator(
    IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCalling,
    INotImplementedSelector notImplementedSelector)
    : IQueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementIdSelectorString selector => GenerateJsLineByIdString(jsObjectName.Name, selector.Id),
            ElementIdSelectorDelegate => string.Empty,
            _ => notImplementedSelector.ThrowNotImplementedException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsLineByIdString(string jsObjectName, string id)
        => $"const {jsObjectName} = {getStoredElementByIdAsArrayCalling.Generate(id, jsObjectName)};";
}
