using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysValueByTemplateJsGenerator(
    IGetTemplateCall getTemplateCall,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringAlwaysValueByTemplateJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            InsertElementTemplateSelectorId templateSelectorId => GenerateJsById(jsObjectName.Name, templateSelectorId.TemplateId),
            _ => notImplementedSelector.ThrowException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsById(string jsObjectName, string templateId)
        => $"const {jsObjectName} = {getTemplateCall.Generate(templateId)};";
}