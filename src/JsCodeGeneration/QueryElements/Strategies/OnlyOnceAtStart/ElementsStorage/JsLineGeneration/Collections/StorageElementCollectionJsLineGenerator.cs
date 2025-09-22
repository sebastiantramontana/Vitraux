using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal class StorageElementCollectionJsLineGenerator(
    IStorageElementCollectionJsLineGeneratorByTemplate jsLineGeneratorByTemplate,
    IStorageElementCollectionJsLineGeneratorByUri jsLineGeneratorByUri,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementCollectionJsLineGenerator
{
    public string Generate(JsElementObjectName collectionObjectName)
    {
        return collectionObjectName.AssociatedSelector switch
        {
            TemplateInsertionSelectorBase => jsLineGeneratorByTemplate.Generate(collectionObjectName),
            UriInsertionSelectorBase => jsLineGeneratorByUri.Generate(collectionObjectName),
            _ => notImplementedSelector.ThrowException<string>(collectionObjectName.AssociatedSelector)
        };
    }
}
