using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByIdJsGenerator(
    IGetElementByIdAsArrayCall getElementByIdAsArrayCalling,
    INotImplementedCaseGuard notImplementedSelector) 
    : IQueryElementsDeclaringAlwaysByIdJsGenerator
{
    public string GenerateJsCode(string parentElementObjectName, JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            ElementIdSelectorString elementIdSelector => GenerateJsLineByIdString(jsElementObjectName.Name, elementIdSelector.Id),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };

    private string GenerateJsLineByIdString(string jsElementObjectName, string id)
        => $"const {jsElementObjectName} = {getElementByIdAsArrayCalling.Generate(id)};";
}
