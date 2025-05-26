using System.Text;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionsJsGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    IUpdateCollectionJsCodeGenerator updateCollectionJsCodeGenerator)
    : ICollectionsJsGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<CollectionObjectName> collections, IJsGenerator jsGenerator)
        => collections.Aggregate(new StringBuilder(), (sb, collection) =>
        {
            return collection
                    .AssociatedElementNames
                    .Aggregate(sb, (sb2, associatedElement) =>
                    {
                        var updateCollectionjsCode = updateCollectionJsCodeGenerator.GenerateJs(parentObjectName, collection.Name, associatedElement, jsGenerator);
                        var propertyCheckerJsCode = propertyChecker.GenerateJs(parentObjectName, collection.Name, updateCollectionjsCode);
                        return sb2.AppendLine(propertyCheckerJsCode);
                    });
        })
        .ToString()
        .TrimEnd();
}



