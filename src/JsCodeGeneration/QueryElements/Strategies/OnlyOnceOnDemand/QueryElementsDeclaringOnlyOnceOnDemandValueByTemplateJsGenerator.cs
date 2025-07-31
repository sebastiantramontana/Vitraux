using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandValueByTemplateJsGenerator(
    IGetStoredTemplateCall getStoredElementByTemplateCall,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringOnlyOnceOnDemandValueByTemplateJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            InsertElementTemplateSelectorId templateSelectorId => GenerateJsById(jsObjectName.Name, templateSelectorId),
            _ => notImplementedSelector.ThrowException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsById(string jsObjectName, InsertElementTemplateSelectorId templateSelectorId)
        => $"const {jsObjectName} = {getStoredElementByTemplateCall.Generate(templateSelectorId.TemplateId, jsObjectName)};";
}
