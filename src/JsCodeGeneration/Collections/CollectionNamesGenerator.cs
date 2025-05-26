using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionNamesGenerator : ICollectionNamesGenerator
{
    public IEnumerable<CollectionObjectName> Generate(IEnumerable<CollectionData> collections, IEnumerable<JsObjectName> allJsObjectNames)
        => collections
            .Select((col, indexAsPostfix) => CreateCollectionObjectName($"collection{indexAsPostfix}", col, allJsObjectNames))
            .RunNow();

    private static CollectionObjectName CreateCollectionObjectName(string collectionObjectName, CollectionData data, IEnumerable<JsObjectName> allJsObjectNames)
    {
        var elementTargets = data.Targets.OfType<CollectionElementTarget>();
        var elementObjectPairNames = elementTargets.Select(t => CreateJsCollectionElementObjectPairNamesNameByTarget(t, allJsObjectNames));

        return new(collectionObjectName, elementObjectPairNames);
    }

    private static JsCollectionElementObjectPairNames CreateJsCollectionElementObjectPairNamesNameByTarget(CollectionElementTarget target, IEnumerable<JsObjectName> allJsObjectNames)
    {
        var appendToElementObjectName = allJsObjectNames.Single(e => e.AssociatedSelector == target.AppendToElementSelector);
        var elementToInsertObjectName = allJsObjectNames.Single(e => e.AssociatedSelector == target.InsertionSelector);

        return new(appendToElementObjectName.Name, elementToInsertObjectName.Name, target);
    }
}
