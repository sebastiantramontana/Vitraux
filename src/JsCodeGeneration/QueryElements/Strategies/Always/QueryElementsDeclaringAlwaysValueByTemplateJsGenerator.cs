using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysValueByTemplateJsGenerator(
    IGetTemplateCall getTemplateCall,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringAlwaysValueByTemplateJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            InsertElementTemplateSelectorId templateSelectorId => GenerateJsById(jsElementObjectName.Name, templateSelectorId.TemplateId),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };

    private string GenerateJsById(string jsElementObjectName, string templateId)
        => $"const {jsElementObjectName} = {getTemplateCall.Generate(templateId)};";
}