using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionNamesGenerator : ICollectionNamesGenerator
{
    const string CollectionObjectNamePrefix = "c";

    public IEnumerable<FullCollectionObjectName> Generate(IEnumerable<CollectionData> collections, IEnumerable<JsObjectName> allJsElementObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator)
        => collections.Select((col, indexAsPostfix) => CreateCollectionObjectName(GenerateObjName(indexAsPostfix), col, allJsElementObjectNames, jsFullObjectNamesGenerator));

    private static string GenerateObjName(int indexAsPostfix)
        => $"{CollectionObjectNamePrefix}{indexAsPostfix}";

    private static FullCollectionObjectName CreateCollectionObjectName(string collectionObjectName, CollectionData collectionData, IEnumerable<JsObjectName> allJsElementObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator)
    {
        var elementTargets = collectionData.Targets.OfType<CollectionElementTarget>();
        var elementObjectPairNames = elementTargets.Select(t => CreateJsCollectionElementObjectPairNamesNameByTarget(t, allJsElementObjectNames, jsFullObjectNamesGenerator));

        return new(collectionObjectName, elementObjectPairNames, collectionData);
    }

    private static JsCollectionElementObjectPairNames CreateJsCollectionElementObjectPairNamesNameByTarget(CollectionElementTarget target, IEnumerable<JsObjectName> allJsElementObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator)
    {
        var appendToElementObjectName = allJsElementObjectNames.Single(e => e.AssociatedSelector == target.AppendToElementSelector);
        var elementToInsertObjectName = allJsElementObjectNames.Single(e => e.AssociatedSelector == target.InsertionSelector);
        var children = jsFullObjectNamesGenerator.Generate(target.Data, allJsElementObjectNames);

        return new(appendToElementObjectName.Name, elementToInsertObjectName.Name, target, children);
    }
}