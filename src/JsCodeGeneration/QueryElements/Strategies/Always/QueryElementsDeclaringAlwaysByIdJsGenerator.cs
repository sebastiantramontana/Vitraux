using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByIdJsGenerator(
    IGetElementByIdAsArrayCall getElementByIdAsArrayCalling,
    INotImplementedCaseGuard notImplementedSelector) 
    : IQueryElementsDeclaringAlwaysByIdJsGenerator
{
    public string GenerateJsCode(string parentElementObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementIdSelectorString elementIdSelector => GenerateJsLineByIdString(jsObjectName.Name, elementIdSelector.Id),
            ElementIdSelectorDelegate => string.Empty,
            _ => notImplementedSelector.ThrowException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsLineByIdString(string jsObjectName, string id)
        => $"const {jsObjectName} = {getElementByIdAsArrayCalling.Generate(id)};";
}
