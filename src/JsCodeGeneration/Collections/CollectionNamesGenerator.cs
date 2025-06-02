using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionNamesGenerator : ICollectionNamesGenerator
{
    const string CollectionObjectNamePrefix = "c";

    public IEnumerable<CollectionObjectName> Generate(IEnumerable<CollectionData> collections, IEnumerable<JsObjectName> allJsElementObjectNames)
        => collections.Select((col, indexAsPostfix) => CreateCollectionObjectName(GenerateObjName(indexAsPostfix), col, allJsElementObjectNames));

    private static string GenerateObjName(int indexAsPostfix)
        => $"{CollectionObjectNamePrefix}{indexAsPostfix}";

    private static CollectionObjectName CreateCollectionObjectName(string collectionObjectName, CollectionData data, IEnumerable<JsObjectName> allJsElementObjectNames)
    {
        var elementTargets = data.Targets.OfType<CollectionElementTarget>();
        var elementObjectPairNames = elementTargets.Select(t => CreateJsCollectionElementObjectPairNamesNameByTarget(t, allJsElementObjectNames));

        return new(collectionObjectName, elementObjectPairNames);
    }

    private static JsCollectionElementObjectPairNames CreateJsCollectionElementObjectPairNamesNameByTarget(CollectionElementTarget target, IEnumerable<JsObjectName> allJsElementObjectNames)
    {
        var appendToElementObjectName = allJsElementObjectNames.Single(e => e.AssociatedSelector == target.AppendToElementSelector);
        var elementToInsertObjectName = allJsElementObjectNames.Single(e => e.AssociatedSelector == target.InsertionSelector);

        return new(appendToElementObjectName.Name, elementToInsertObjectName.Name, target);
    }
}
