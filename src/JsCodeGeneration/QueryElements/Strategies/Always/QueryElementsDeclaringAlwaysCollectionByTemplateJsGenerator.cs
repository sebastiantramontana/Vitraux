using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCollectionByTemplateJsGenerator(IGetTemplateCall getTemplateCall)
    : IQueryElementsDeclaringAlwaysCollectionByTemplateJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            TemplateInsertionSelectorId templateSelectorId => GenerateJsById(jsObjectName.Name, templateSelectorId.TemplateId),
            TemplateInsertionSelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {jsObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };

    private string GenerateJsById(string jsObjectName, string templateId)
        => $"const {jsObjectName} = {getTemplateCall.Generate(templateId)};";
}
