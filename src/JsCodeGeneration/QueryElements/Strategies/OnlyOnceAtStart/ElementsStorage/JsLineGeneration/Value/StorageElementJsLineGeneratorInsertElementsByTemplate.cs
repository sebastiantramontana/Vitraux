using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorInsertElementsByTemplate(
    IStorageElementJsLineGeneratorByTemplate storageElementJsLineGeneratorByTemplate,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementJsLineGeneratorInsertElementsByTemplate
{
    public string Generate(JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            InsertElementTemplateSelectorId templateSelector => storageElementJsLineGeneratorByTemplate.Generate(templateSelector.TemplateId, jsObjectName.Name),
            _ => notImplementedSelector.ThrowException<string>(jsObjectName.AssociatedSelector)
        };
}
