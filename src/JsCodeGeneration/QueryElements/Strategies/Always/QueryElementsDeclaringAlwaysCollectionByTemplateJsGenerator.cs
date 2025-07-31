using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCollectionByTemplateJsGenerator(
    IGetTemplateCall getTemplateCall,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringAlwaysCollectionByTemplateJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            TemplateInsertionSelectorId templateSelectorId => GenerateJsById(jsObjectName.Name, templateSelectorId.TemplateId),
            _ => notImplementedSelector.ThrowException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsById(string jsObjectName, string templateId)
        => $"const {jsObjectName} = {getTemplateCall.Generate(templateId)};";
}
