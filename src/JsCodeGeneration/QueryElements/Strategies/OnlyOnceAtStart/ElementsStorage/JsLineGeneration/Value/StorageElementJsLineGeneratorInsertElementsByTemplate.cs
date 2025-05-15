using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorInsertElementsByTemplate(
    IStorageElementJsLineGeneratorByTemplate storageElementJsLineGeneratorByTemplate)
    : IStorageElementJsLineGeneratorInsertElementsByTemplate
{
    public string Generate(JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            InsertElementTemplateSelectorId templateSelector => storageElementJsLineGeneratorByTemplate.Generate(templateSelector.TemplateId, jsObjectName.Name),
            InsertElementTemplateSelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {jsObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };
}
