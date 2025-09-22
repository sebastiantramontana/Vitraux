using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorInsertElementsByTemplate(
    IStorageElementJsLineGeneratorByTemplate storageElementJsLineGeneratorByTemplate,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementJsLineGeneratorInsertElementsByTemplate
{
    public string Generate(JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            InsertElementTemplateSelectorId templateSelector => storageElementJsLineGeneratorByTemplate.Generate(templateSelector.TemplateId, jsElementObjectName.Name),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };
}
