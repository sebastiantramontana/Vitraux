using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal class StorageElementCollectionJsLineGenerator(
    IStorageElementCollectionJsLineGeneratorByTemplate jsLineGeneratorByTemplate,
    IStorageElementCollectionJsLineGeneratorByUri jsLineGeneratorByUri): IStorageElementCollectionJsLineGenerator
{
    public string Generate(JsObjectName collectionObjectName)
    {
        return collectionObjectName.AssociatedSelector switch
        {
            TemplateInsertionSelectorBase => jsLineGeneratorByTemplate.Generate(collectionObjectName),
            UriInsertionSelectorBase => jsLineGeneratorByUri.Generate(collectionObjectName),
            _ => throw new NotImplementedException($"InsertionSelection type {collectionObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };
    }
}
