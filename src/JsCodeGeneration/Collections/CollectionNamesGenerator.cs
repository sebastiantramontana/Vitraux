using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionNamesGenerator : ICollectionNamesGenerator
{
    const string CollectionObjectNamePrefix = "c";

    public IEnumerable<FullCollectionObjectName> Generate(IEnumerable<CollectionData> collections, IEnumerable<JsObjectName> currentElementJsObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator, int nestingLevel)
        => collections.Select((col, indexAsPostfix) => CreateCollectionObjectName(GenerateObjName(indexAsPostfix), col, currentElementJsObjectNames, jsFullObjectNamesGenerator, nestingLevel));

    private static string GenerateObjName(int indexAsPostfix)
        => $"{CollectionObjectNamePrefix}{indexAsPostfix}";

    private static FullCollectionObjectName CreateCollectionObjectName(string collectionObjectName, CollectionData collectionData, IEnumerable<JsObjectName> currentElementJsObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator, int nestingLevel)
    {
        var elementTargets = collectionData.Targets.OfType<CollectionElementTarget>();
        var elementObjectPairNames = elementTargets.Select(t => CreateJsCollectionElementObjectPairNamesNameByTarget(t, currentElementJsObjectNames, jsFullObjectNamesGenerator, collectionObjectName, nestingLevel));

        return new(collectionObjectName, elementObjectPairNames, collectionData);
    }

    private static JsCollectionElementObjectPairNames CreateJsCollectionElementObjectPairNamesNameByTarget(CollectionElementTarget target, IEnumerable<JsObjectName> currentElementJsObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator, string collectionObjectName, int nestingLevel)
    {
        var appendToElementObjectName = currentElementJsObjectNames.Single(e => e.AssociatedSelector == target.AppendToElementSelector);
        var elementToInsertObjectName = currentElementJsObjectNames.Single(e => e.AssociatedSelector == target.InsertionSelector);
        var elenementNamePrefix = GenerateElementNamePrefix(collectionObjectName, nestingLevel);
        var children = jsFullObjectNamesGenerator.Generate(elenementNamePrefix, target.Data, nestingLevel + 1);

        return new(appendToElementObjectName.Name, elementToInsertObjectName.Name, target, children);
    }

    private static string GenerateElementNamePrefix(string collectionObjectName, int nestingLevel)
        => $"n{nestingLevel}_{collectionObjectName}";
}