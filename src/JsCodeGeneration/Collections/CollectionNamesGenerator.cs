using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionNamesGenerator : ICollectionNamesGenerator
{
    const string CollectionObjectNamePrefix = "c";

    public IEnumerable<FullCollectionObjectName> Generate(IEnumerable<CollectionData> collections, IEnumerable<JsElementObjectName> currentElementJsObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator, int nestingLevel)
        => collections.Select((col, indexAsPostfix) => CreateCollectionObjectName(GenerateObjName(indexAsPostfix), col, currentElementJsObjectNames, jsFullObjectNamesGenerator, nestingLevel));

    private static string GenerateObjName(int indexAsPostfix)
        => $"{CollectionObjectNamePrefix}{indexAsPostfix}";

    private static FullCollectionObjectName CreateCollectionObjectName(string collectionObjectName, CollectionData collectionData, IEnumerable<JsElementObjectName> currentElementJsObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator, int nestingLevel)
    {
        var elementObjectPairNames = CreateElementObjectPairNames(collectionObjectName, collectionData, currentElementJsObjectNames, jsFullObjectNamesGenerator, nestingLevel);
        var customJsNames = CreateCustomJsNames(collectionData);
        var collectionNames = elementObjectPairNames.Concat(customJsNames);

        return new(collectionObjectName, collectionNames, collectionData);
    }

    private static IEnumerable<JsCollectionNames> CreateElementObjectPairNames(string collectionObjectName, CollectionData collectionData, IEnumerable<JsElementObjectName> currentElementJsObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator, int nestingLevel)
    {
        var elementTargets = GetCollectionElementTargets(collectionData);
        return elementTargets.Select(t => CreateJsCollectionElementObjectPairNamesNameByTarget(t, currentElementJsObjectNames, jsFullObjectNamesGenerator, collectionObjectName, nestingLevel));
    }

    private static IEnumerable<JsCollectionNames> CreateCustomJsNames(CollectionData collectionData)
    {
        var customJsTargets = JsCollectionCustomJsNames(collectionData);
        return customJsTargets.Select(t => new JsCollectionCustomJsNames(t));
    }

    private static IEnumerable<CollectionElementTarget> GetCollectionElementTargets(CollectionData collectionData)
        => collectionData.Targets.OfType<CollectionElementTarget>();

    private static IEnumerable<CustomJsCollectionTarget> JsCollectionCustomJsNames(CollectionData collectionData)
        => collectionData.Targets.OfType<CustomJsCollectionTarget>();

    private static JsCollectionElementObjectPairNames CreateJsCollectionElementObjectPairNamesNameByTarget(CollectionElementTarget target, IEnumerable<JsElementObjectName> currentElementJsObjectNames, IJsFullObjectNamesGenerator jsFullObjectNamesGenerator, string collectionObjectName, int nestingLevel)
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