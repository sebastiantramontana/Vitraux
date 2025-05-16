using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandCollectionByTemplateJsGenerator(
    IGetStoredTemplateCall getStoredElementByTemplateCall,
    INotImplementedSelector notImplementedSelector) 
    : IQueryElementsDeclaringOnlyOnceOnDemandCollectionByTemplateJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            TemplateInsertionSelectorId templateSelectorId => GenerateJsById(jsObjectName.Name, templateSelectorId),
            InsertElementTemplateSelectorDelegate => string.Empty,
            _ => notImplementedSelector.ThrowNotImplementedException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsById(string jsObjectName, TemplateInsertionSelectorId templateSelectorId)
        => $"const {jsObjectName} = {getStoredElementByTemplateCall.Generate(templateSelectorId.TemplateId, jsObjectName)};";
}
